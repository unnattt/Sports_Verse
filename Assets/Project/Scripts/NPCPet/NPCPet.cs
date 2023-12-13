using Sportsverse.World;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Sportsverse.World
{


    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCPet : MonoBehaviour
    {
        public Bone targetInteractable;
        public Vector3 ownerPosition;

        [HideInInspector]
        public NavMeshAgent navMeshAgent;
        private PetState currentState;

        public Transform holder;

        public Action OnTargetReached, OnTargetReturned;

        private void Start()
        {
            targetInteractable.OnBoneThrown += OnBoneThrown;
            navMeshAgent = GetComponent<NavMeshAgent>();
            currentState = new IdleState();
        }

        private void OnBoneThrown(Vector3 originPosition)
        {
            ownerPosition = originPosition;
        }

        private void OnDestroy()
        {
            targetInteractable.OnBoneThrown -= OnBoneThrown;
        }

        private void Update()
        {
            currentState = currentState.Execute(this, navMeshAgent);
        }

    }

    public interface PetState
    {
        PetState Execute(NPCPet pet, NavMeshAgent agent);
    }

    public class IdleState : PetState
    {
        public PetState Execute(NPCPet pet, NavMeshAgent agent)
        {
            if (pet.targetInteractable.isThrown)
            {
                agent.SetDestination(pet.targetInteractable.GetInteractable().transform.position);
                return new ChasingState();
            }
            return this;
        }
    }

    public class ChasingState : PetState
    {
        float currentTime = 0, thresholdTime = 1f;



        public PetState Execute(NPCPet pet, NavMeshAgent agent)
        {

            currentTime += Time.deltaTime;

            if (currentTime > thresholdTime)
            {
                currentTime = 0;
                agent.SetDestination(pet.targetInteractable.GetInteractable().transform.position);
            }



            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                pet.OnTargetReached?.Invoke();
                pet.targetInteractable.Interact(pet.holder.gameObject);
                agent.SetDestination(pet.ownerPosition);
                return new ReturningState();
            }


           
            return this;
        }
    }

    public class ReturningState : PetState
    {
        public PetState Execute(NPCPet pet, NavMeshAgent agent)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                pet.OnTargetReturned?.Invoke();
                pet.targetInteractable.transform.SetParent(null);
                pet.targetInteractable.ResetObject();
                return new IdleState();
            }


            return this;
        }
    }

}
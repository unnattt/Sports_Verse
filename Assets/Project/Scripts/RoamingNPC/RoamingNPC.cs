using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Sportsverse.World
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class RoamingNPC : MonoBehaviour
    {
        public float roamRadius = 10f;
        public float MinTimeToWaitAtDestination = 2f;
        public float MaxTimeToWaitAtDestination = 10f;

        public Transform CharactersParent;

        private NavMeshAgent navMeshAgent;
        private bool isWaiting;

        public Action OnNPCWaitStarted;
        public Action OnNPCWaitEnded;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            SetRandomPlayer();
            SetRandomDestination();
        }



        private void Update()
        {
            if (!isWaiting && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                StartCoroutine(WaitAtDestination(Random.Range(MinTimeToWaitAtDestination, MaxTimeToWaitAtDestination)));
            }
        }

        private void SetRandomDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;

            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(randomDirection, out navMeshHit, roamRadius, 1);
            Vector3 finalPosition = navMeshHit.position;

            navMeshAgent.SetDestination(finalPosition);

            bool shouldWalk = Random.Range(0, 10) < 8 ? true : false;

            if (shouldWalk)
            {
                navMeshAgent.speed = 1f;
            }
            else
            {
                navMeshAgent.speed = 5f;
            }

        }

        private IEnumerator WaitAtDestination(float waitTime)
        {
            isWaiting = true;

            OnNPCWaitStarted?.Invoke();
            yield return new WaitForSeconds(waitTime);



            isWaiting = false;
            OnNPCWaitEnded?.Invoke();
            SetRandomDestination();
        }

        public void SetRandomPlayer()
        {
            for(int i = 1; i < CharactersParent.childCount;i++)
            {
                CharactersParent.GetChild(i).gameObject.SetActive(false);
            }

            int selectedIndex = Random.Range(1, CharactersParent.childCount);

            CharactersParent.GetChild(selectedIndex).gameObject.SetActive(true);
        }
    }

}
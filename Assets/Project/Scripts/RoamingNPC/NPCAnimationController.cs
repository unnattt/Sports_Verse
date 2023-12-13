using Sportsverse.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Sportsverse.World
{

    public class NPCAnimationController : MonoBehaviour
    {
        public Animator controller;
        public NavMeshAgent agent;
        public RoamingNPC roamingNPC;

        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponentInChildren<Animator>();
            agent = GetComponent<NavMeshAgent>();
            roamingNPC = GetComponent<RoamingNPC>();
            controller.SetFloat("Speed", 0);

            roamingNPC.OnNPCWaitStarted += OnNPCWaiting;
            roamingNPC.OnNPCWaitEnded += OnNPCWaitEnded;

        }


        private void OnDestroy()
        {
            roamingNPC.OnNPCWaitStarted -= OnNPCWaiting;
            roamingNPC.OnNPCWaitEnded -= OnNPCWaitEnded;

        }

        private void OnNPCWaiting()
        {
            controller.SetFloat("Speed", 0);
        }
        private void OnNPCWaitEnded()
        {
        }

        Vector3 previousPosition; float curSpeed;
        void Update()
        {
            Vector3 curMove = transform.position - previousPosition;
            curSpeed = curMove.magnitude / Time.deltaTime;
            previousPosition = transform.position;

            controller.SetFloat("Speed", Mathf.Clamp01(curSpeed / 2f));
        }
    }
}

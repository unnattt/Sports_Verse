using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sportsverse.World
{
    public class JoyrideController : MonoBehaviour
    {
        public CinemachineDollyCart cart;
        public float Speed;
        public Transform PlayerHolder;
        public Transform ExitPoint;

        XRNetworkPlayer player;

        bool isRideInProgress;

        public Action OnRideStarted, OnRideEnded;

        public bool IsRideInProgress { get => isRideInProgress; }

        private void Awake()
        {
            cart = GetComponentInChildren<CinemachineDollyCart>();

        }




        public void StartRide(XRNetworkPlayer networkPlayer)
        {
            if (!isRideInProgress)
            {
                networkPlayer.TogglePlayerControl(false);
                player = networkPlayer;

                player.transform.parent = PlayerHolder;
                player.transform.localPosition = Vector3.zero;
                player.transform.localRotation = Quaternion.identity;
                
                StartCoroutine(JoyrideRoutine());
            }
        }

        IEnumerator JoyrideRoutine()
        {
            isRideInProgress = true;
            OnRideStarted?.Invoke();

            cart.m_Position = 0;

            float pathLength = cart.m_Path.PathLength;

            while (cart.m_Position / pathLength < 1f)
            {
                if (cart.m_Position / pathLength > 0.95f)
                {
                    Speed = 1f;
                }

                Debug.Log(cart.m_Position / cart.m_Path.PathLength);

                cart.m_Position += Time.deltaTime * Speed;
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            player.transform.position = ExitPoint.position;
            player.transform.rotation = ExitPoint.rotation;
            player.transform.parent = null;

            player.TogglePlayerControl(true);


            isRideInProgress = false;
            OnRideEnded?.Invoke();

        }


    }
}
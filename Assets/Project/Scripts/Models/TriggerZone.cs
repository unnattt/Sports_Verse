using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sportsverse
{
    public class TriggerZone : MonoBehaviour
    {
        public UnityEvent<XRNetworkPlayer> OnPlayerInsideZone;
        public UnityEvent<XRNetworkPlayer> OnPlayerOutsideZone;

        private void OnTriggerEnter(Collider other)
        {
            XRNetworkPlayer networkPlayer = other.GetComponent<XRNetworkPlayer>();
            if (other.GetComponent<XRPlayer>() != null || (networkPlayer != null))
            {
                OnPlayerInsideZone.Invoke(networkPlayer);
                PlayerInsideZone(networkPlayer);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            XRNetworkPlayer networkPlayer = other.GetComponent<XRNetworkPlayer>();
            if (other.GetComponent<XRPlayer>() != null || (networkPlayer != null))
            {
                OnPlayerOutsideZone.Invoke(networkPlayer);
                PlayerOutsideZone(networkPlayer);
            }
        }

        public virtual void PlayerInsideZone(XRNetworkPlayer networkPlayer)
        {
        }
        public virtual void PlayerOutsideZone(XRNetworkPlayer networkPlayer)
        {
        }
    }
}
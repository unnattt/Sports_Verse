using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace CanKnockdown
{
    public class Ball : MonoBehaviour
    {
        public UnityEvent<Ball> onBallGrabbed;
        public UnityEvent<Ball> onBallReleased;

        public XRGrabInteractable grabInteractable;
        public Rigidbody rb;

        void OnEnable()
        {
            grabInteractable.selectEntered.AddListener(GrabBall);
            grabInteractable.selectExited.AddListener(ReleaseBall);
        }
        private void OnDisable()
        {
            grabInteractable.selectEntered.RemoveListener(GrabBall);
            grabInteractable.selectExited.RemoveListener(ReleaseBall);
        }


        private void GrabBall(SelectEnterEventArgs arg0)
        {
            onBallGrabbed?.Invoke(this);
        }

        private void ReleaseBall(SelectExitEventArgs arg0)
        {
            onBallReleased?.Invoke(this);
        }


    }
}
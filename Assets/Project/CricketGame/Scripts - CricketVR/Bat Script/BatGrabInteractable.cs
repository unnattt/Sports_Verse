using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz;
using UnityEngine.XR.Interaction.Toolkit;

namespace Yudiz.VRCricket.Core
{
    public class BatGrabInteractable : XRGrabInteractable
    {
        private Rigidbody rb;
        private FixedJoint fixedJoint;

        private float HapticsAmplitude = 0.7f;
        private float HapticsDuration = 0.3f;

        [SerializeField] private GameObject lefthand;
        [SerializeField] private GameObject rightHand;

        private XRGrabInteractable xRGrabInteractable;
        private XRBaseController xRBaseController;

        private const String grabbedBatLayer = "GrabbedBat";

        private bool isGrabbed;

        void Start()
        {
            xRGrabInteractable = GetComponent<XRGrabInteractable>();
            xRGrabInteractable.selectEntered.AddListener(OnGrab);

            rb = GetComponent<Rigidbody>();

            isGrabbed = true;
        }

        private void OnGrab(SelectEnterEventArgs arg0)
        {
            Debug.Log("Bat Grabbed");

            rb.isKinematic = false;

            XRBaseController xRController = arg0.interactorObject.transform.GetComponent<XRBaseController>();
            xRBaseController = xRController;

            if (xRController.gameObject.name.Contains("Left"))
            {
                Debug.Log("Left Grabbed");

                lefthand.SetActive(true);
                GameEvents.disableLeftHand?.Invoke();
            }
            else
            {
                Debug.Log("Right Grabbed");

                rightHand.SetActive(true);
                GameEvents.disableRightHand?.Invoke();
            }

            fixedJoint = arg0.interactorObject.transform.GetComponent<FixedJoint>();
            Invoke(nameof(AttachFixedJoint), 0.1f);

            ChangeBatLayers();
        }

        private void AttachFixedJoint()
        {
            if (fixedJoint != null && isGrabbed)
            {
                Debug.Log("Fixed Joint Found");
                fixedJoint.connectedBody = rb;

                GameManager.inst.StartGame();
                isGrabbed = false;

                movementType = MovementType.VelocityTracking;
            }
            else
            {
                Debug.Log("No Fixed Joint");
            }
        }

        public void Triggerhaptics()
        {
            if (xRBaseController != null)
            {
                Debug.Log("XRController Found");
                xRBaseController.SendHapticImpulse(HapticsAmplitude, HapticsDuration);
            }
            else
            {
                Debug.Log("XRController Not Found");
            }
        }

        private void ChangeBatLayers()
        {
            int BatLayer = LayerMask.NameToLayer(grabbedBatLayer);
            gameObject.layer = BatLayer;
            fixedJoint.gameObject.layer = BatLayer;
        }
    }
}
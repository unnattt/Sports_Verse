using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.DartController
{

    public class DartMovement : MonoBehaviour
    {
        //public float force;      // Initial throw force

        public Rigidbody dartRigidbody;
        //public Vector3 dragStartPosition;
        //public Vector3 dragEndPosition;
        public bool isMove = false;
        public float Currenttime = 0.1f;
        public float Startime;
        public float Endtime;
        public bool isThrowed = false;

        private float dragDistance;
        private bool isDragging = false;
        public bool isdartthrow = false;
        public Collider _collider;
        public float Distance;

        public GameObject particals;

        public Transform picupOne;
        public Transform picupTwo;


        public XRGrabInteractable grabInteractable;

        private void Awake()
        {

            _collider = GetComponentInChildren<Collider>();
        }

        public void Start()
        {
            grabInteractable.hoverEntered.AddListener(Hover);
            grabInteractable.selectEntered.AddListener(getCurrentDart);
            grabInteractable.selectExited.AddListener(ReleaseDart);
        }

        private void Hover(HoverEnterEventArgs arg0)
        {
            Debug.Log("----Hover----");
            if (arg0.interactorObject.transform.name == "RightDirectInteractor" || arg0.interactorObject.transform.name == "Right Ray Controller")
            {

                grabInteractable.attachTransform = picupTwo;


            }
            else
            {
                grabInteractable.attachTransform = picupOne;
            }
        }

        private void getCurrentDart(SelectEnterEventArgs arg0)
        {

            GameManager._instance.currentDartPos = transform.position;
            GameManager._instance.currentDart = this;
            GameManager._instance.DisableDartGreb();
        }

        private void FixedUpdate()
        {
            if (isdartthrow == true)
            {
                transform.rotation = Quaternion.LookRotation(dartRigidbody.velocity);
            }

        }

        //private void StartDragging()
        //{

        //    isDragging = true;
        //    dragStartPosition = Input.mousePosition;

        //}

        //private void ContinueDragging()
        //{

        //    if (!isDragging)
        //        return;
        //    dragDistance = Vector3.Distance(dragStartPosition, dragEndPosition);

        //}
        //---VR---
        public void ReleaseDart(SelectExitEventArgs arg0)
        {
            //grabInteractable.attachTransform = picupOne;
            particals.SetActive(true);
            isDragging = false;
            dartRigidbody.isKinematic = false;
            isdartthrow = true;
            GameManager._instance.ActivetDartGreb();
        }
        public async void OnCollisionEnter(Collision collision)
        {
            if (isdartthrow)
            {
                particals.SetActive(false);
                Distance = Vector3.Distance(GameManager._instance.XROrigin.transform.position, this.transform.position);
                GameManager._instance.ThrowedDartList.Add(this.gameObject);
                GameManager._instance.NormalResetDart();
                dartRigidbody.isKinematic = true;
                await Task.Delay(2000);
                if (GameManager._instance.currentDartCount == 0)
                {
                    GameManager._instance.TurnChangResetDart();
                }

            }
        }

    }
}

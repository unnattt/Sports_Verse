using Sportsverse.World;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Sportsverse.World
{

    public class Bone : MonoBehaviour, IInteractable
    {
        public Action<Vector3> OnBoneThrown;

        public bool isThrown = false;
        public bool hasLanded = false;
        private Rigidbody rb;
        private Vector3 position;
        private Vector3 initialPosition;

        XRGrabInteractable grabInteractable;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            
            grabInteractable = GetComponent<XRGrabInteractable>();

            initialPosition = transform.position;

            grabInteractable.selectEntered.AddListener(OnInteractableEntered);
            grabInteractable.selectExited.AddListener(OnInteractableExited);
        }

       

        private void OnDestroy()
        {
            grabInteractable.selectExited.RemoveListener(OnInteractableExited);
            grabInteractable.selectEntered.RemoveListener(OnInteractableEntered);

        }


        private void OnInteractableEntered(SelectEnterEventArgs arg0)
        {
#if UNITY_EDITOR
            initialPosition = new Vector3(10,0,10);
#else
            initialPosition = transform.position;
#endif
        }

        private void OnInteractableExited(SelectExitEventArgs arg0)
        {
#if UNITY_EDITOR
            Throw(new Vector3(10, 0, 10));
#else
            Throw(transform.position + transform.forward.normalized * (0.1f));
#endif
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnInteractableExited(null);
            }
        }

        public void Throw(Vector3 throwerPosition)
        {
            if (!isThrown)
            {
                position = throwerPosition;
                isThrown = true;
                StartCoroutine(ThrowCheckRoutine());
            }
        }

        IEnumerator ThrowCheckRoutine()
        {
            yield return new WaitForSeconds(0.2f);

            OnBoneThrown?.Invoke(position);
        }


        IEnumerator LostCheckRoutine()
        {
            yield return new WaitForSeconds(5f);

            transform.position = initialPosition;


        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isThrown)
            {
                hasLanded = true;
                rb.velocity = Vector3.zero;
            }
        }

        public void Interact(GameObject interactor)
        {
            rb.isKinematic = true;
            transform.SetParent(interactor.transform);
            transform.localPosition = Vector3.zero;

        }
        public GameObject GetInteractable()
        {
            return gameObject;
        }

        public void ResetObject()
        {
            isThrown = false;
            hasLanded = false;
            rb.isKinematic = false;
        }


    }

}
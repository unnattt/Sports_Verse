using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

namespace Sportsverse.FireworksGun
{
    public class FireworksGun : MonoBehaviour
    {
        [SerializeField] VisualEffect crackerVFX;
        [SerializeField] Transform crackerSpawnPoint;
        [SerializeField] private float vfxTime;
        XRGrabInteractable grabInteractable;
        [SerializeField] private AudioClip firecrackerAudio;
        [SerializeField] private AudioSource firecrackerAudioSource;

        private void Awake()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            grabInteractable.selectEntered.AddListener(OnGunGrabbed);
            grabInteractable.selectExited.AddListener(OnGunReleased);
        }
        private void OnDisable()
        {
            grabInteractable.selectEntered.RemoveListener(OnGunGrabbed);
            grabInteractable.selectExited.RemoveListener(OnGunReleased);
        }


        private void OnGunGrabbed(SelectEnterEventArgs arg0)
        {
            ActionBasedController controller = arg0.interactorObject.transform.GetComponent<ActionBasedController>();
            controller.activateAction.action.performed += OnGunFired;
        }
        private void OnGunReleased(SelectExitEventArgs arg0)
        {
            ActionBasedController controller = arg0.interactorObject.transform.GetComponent<ActionBasedController>();
            controller.activateAction.action.performed -= OnGunFired;
        }

        private void OnGunFired(InputAction.CallbackContext obj)
        {
            VisualEffect vfx = Instantiate(crackerVFX, crackerSpawnPoint.position, crackerSpawnPoint.rotation);
            Destroy(vfx.gameObject, vfxTime);

            firecrackerAudioSource.PlayOneShot(firecrackerAudio);
        }
    }
}

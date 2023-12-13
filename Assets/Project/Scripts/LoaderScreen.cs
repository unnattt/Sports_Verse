using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.BaseFramework;

namespace Sportsverse.World
{

    public class LoaderScreen : UIScreenView
    {
        public Slider progressBar;
        private Transform xrCameraTransform;


        private void Start()
        {
            xrCameraTransform = FindObjectOfType<Camera>().transform;
            progressBar.value = 0;
            Show();
        }

        Coroutine repositionRoutine;

        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();
            repositionRoutine = StartCoroutine(RepositionRoutine());
            SceneHandler.SceneLoadProgressing += OnSceneLoadProgressing;
        }

        IEnumerator RepositionRoutine()
        {
            yield return new WaitForSeconds(0.1f);

            while (true)
            {
                SetObjectInPlayerCameraForward(transform);

                yield return null;
            }
        }


        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();
            StopCoroutine(repositionRoutine);
            SceneHandler.SceneLoadProgressing += OnSceneLoadProgressing;

        }

        private void OnSceneLoadProgressing(float progress)
        {
            Debug.Log("__" + progress);
            progressBar.value = progress;
        }

        public void SetObjectInPlayerCameraForward(Transform objectToSet, float distanceFromPlayer = 0.5f)
        {
            Vector3 cameraForward = xrCameraTransform.forward;
            cameraForward.y = 0;
            Vector3 newPosition = xrCameraTransform.position + cameraForward * distanceFromPlayer;
            objectToSet.SetPositionAndRotation(newPosition, Quaternion.LookRotation(cameraForward));
        }

    }
}
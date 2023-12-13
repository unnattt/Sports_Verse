using Sportsverse;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Yudiz.BaseFramework;

namespace Yudiz.UI
{
    public class ExitGamePopup : MonoBehaviour
    {
        [SerializeField] private InputActionReference menuButtonReference;
        private Canvas canvas;
        private TrackedDeviceGraphicRaycaster graphicsRaycaster;

        private void Awake()
        {
            //DontDestroyOnLoad(gameObject);
            canvas = GetComponent<Canvas>();
            graphicsRaycaster = GetComponent<TrackedDeviceGraphicRaycaster>();
        }
        private void OnEnable()
        {
            menuButtonReference.action.performed += OnMenuButtonPressed;
        }
        private void OnDisable()
        {
            menuButtonReference.action.performed -= OnMenuButtonPressed;
        }
        private void Start()
        {
            Hide();
        }

        private void OnMenuButtonPressed(InputAction.CallbackContext obj)
        {
            Debug.Log("OnMenuPressed");
            if (XRPlayer.Instance != null)
            {
                XRPlayer.Instance.SetObjectInPlayerCameraForward(transform);
                Show();
            }
        }
        public void OnYesClicked()
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainScene, null);
        }
        public void OnNoClicked()
        {
            Hide();
        }

        public void Show()
        {
            canvas.enabled = true;
            graphicsRaycaster.enabled = true;
        }
        public void Hide()
        {
            canvas.enabled = false;
            graphicsRaycaster.enabled = false;
        }

    }
}
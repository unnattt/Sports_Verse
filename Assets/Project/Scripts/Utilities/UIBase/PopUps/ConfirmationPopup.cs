using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using UnityEngine.SceneManagement;
using System;
using TMPro;

namespace Yudiz.UI
{
    public class ConfirmationPopup : UIScreenView
    {
        [SerializeField]
        private TextMeshProUGUI confirmationText;
        [SerializeField]
        private TextMeshProUGUI yesButtonText;
        [SerializeField]
        private GameObject noButton;

        private Action onYesClickedAction;
        private Action onNoClickedAction;

        Coroutine repositionRoutine;


        private const string yesString = "Yes";
        private const string okString = "Ok";

        #region PUBLIC_METHODS

        public void SetConfirmationData(string message, Action onYesClickedCallback, Action onNoClickedCallback)
        {
            confirmationText.text = message;
            onYesClickedAction = onYesClickedCallback;
            onNoClickedAction = onNoClickedCallback;
            yesButtonText.text = yesString;
            noButton.SetActive(true);
        }
        public void SetConfirmationData(string message, Action onYesClickedCallback)
        {
            confirmationText.text = message;
            onYesClickedAction = onYesClickedCallback;
            yesButtonText.text = okString;
            noButton.SetActive(false);
        }
        public void OnYesClicked()
        {
            onYesClickedAction?.Invoke();
            Hide();
        }
        public void OnNoClicked()
        {
            onNoClickedAction?.Invoke();
            Hide();
        }

        IEnumerator RepositionRoutine()
        {
            yield return new WaitForSeconds(0.01f);

            while (true)
            {
                Sportsverse.GameManager.Instance.networkLocalPlayer.SetObjectInPlayerCameraForward(transform, 0.3f);

                yield return null;
            }
        }
        #endregion

        #region UI_CALLBACKS
        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();
            repositionRoutine = StartCoroutine(RepositionRoutine());

        }
        public override void OnScreenShowAnimationCompleted()
        {
            base.OnScreenShowAnimationCompleted();
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();

            if (repositionRoutine != null)
            {
                StopCoroutine(repositionRoutine);
            }
        }
        #endregion
    }
}



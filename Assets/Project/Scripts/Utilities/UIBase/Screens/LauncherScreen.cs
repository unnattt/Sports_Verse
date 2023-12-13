using Photon.Pun;
using Sportsverse;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.BaseFramework;

namespace Yudiz.UI
{
    public class LauncherScreen : UIScreenView
    {
        #region PUBLIC_VARS
        [SerializeField] private GameObject enterButton;
        [SerializeField] private GameObject enteringText;
        #endregion

        #region PRIVATE_VARS
        [SerializeField] private TMP_InputField nameInputField;
        private string nameEmpty = "Please make sure to enter your name";
        #endregion

        #region UNITY_CALLBACKS
        #endregion

        #region PUBLIC_METHODS
       public void OnClickEnterMetaverseButton()
        {
            // Check if input string is empty or not. If Empty then Show toast
            if (string.IsNullOrEmpty(nameInputField.text))
            {
				ToastPopup popup = PopUpController.Instance.GetPopup<ToastPopup>(PopUpType.ToastPopup);
                if (popup != null)
                {
                    popup.Show();
                    popup.SetData(nameEmpty);
                }
                return;
            }

            // Connect to Photon Servers
            PhotonManager.Instance.SetPlayerNickname(nameInputField.text);

            if (!PhotonNetwork.IsConnected)
            {
                PhotonManager.Instance.ConnectToPhotonServers();
            }
            else
            {
                PhotonManager.Instance.JoinRoom();
            }

            enterButton.SetActive(false);
            enteringText.SetActive(true);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion

        #region BASE_UI_CALLBACKS
        #endregion
    }
}

using Sportsverse;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yudiz.BaseFramework;

namespace Yudiz.UI
{
    public class NavigationBarPopup : UIScreenView
    {
        [SerializeField] private TextMeshProUGUI muteButtonText;
        private bool muteState = false;

        //private void Start()
        //{
        //    if (AgoraVoiceChatHandler.Instance.IsOnMute)
        //    {
        //        muteState = true;
        //        muteButtonText.text = "UNMUTE";
        //    }
        //    else
        //    {
        //        muteState = false;
        //        muteButtonText.text = "MUTE";
        //    }
        //}

        public void ToggleMute()
        {
            if (muteState)
            {
                AgoraVoiceChatHandler.Instance.TurnOnMic();
                muteState = false;
                muteButtonText.text = "MUTE";
            }
            else
            {
                AgoraVoiceChatHandler.Instance.TurnOffMic();
                muteState = true;
                muteButtonText.text = "UNMUTE";
            }
        }

        public override void OnScreenShowCalled()
        {
            Sportsverse.GameManager.Instance.networkLocalPlayer.SetObjectInPlayerCameraForward(transform);
            base.OnScreenShowCalled();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Sportsverse.Data;
using Sportsverse;
using Sportsverse.World;

namespace Yudiz.UI
{
    public class JoyridePopup : UIScreenView
    {
        XRNetworkPlayer activeNetworkPlayer;
        JoyrideController joyrideController;


        #region PUBLIC_METHODS

        public void OnStartRideClicked()
        {
            joyrideController.StartRide(activeNetworkPlayer);
            Hide();
        }

        public void SetActivePlayer(XRNetworkPlayer networkPlayer)
        {
            activeNetworkPlayer = networkPlayer;
        }
        #endregion

        #region PRIVATE_METHODS

        public override void OnAwake()
        {
            base.OnAwake();
            joyrideController = FindObjectOfType<JoyrideController>();
        }

        #endregion

        #region UI_CALLBACKS
        [ContextMenu("Show")]
        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();
        }
        public override void OnScreenShowAnimationCompleted()
        {
            base.OnScreenShowAnimationCompleted();
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();

        }


        #endregion
    }
}



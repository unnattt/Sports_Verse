using Sportsverse;
using Sportsverse.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using Yudiz.UI;

namespace Sportsverse
{
    public class JoyrideTriggerZone : TriggerZone
    {
        [SerializeField] private JoyrideController joyrideController;
        public override void PlayerInsideZone(XRNetworkPlayer networkPlayer)
        {
            base.PlayerInsideZone(networkPlayer);
            if (!joyrideController.IsRideInProgress)
            {
                PopUpController.Instance.GetPopup<JoyridePopup>(PopUpType.JoyridePopup).SetActivePlayer(networkPlayer);
                PopUpController.Instance.ShowPopup(PopUpType.JoyridePopup);
            }
        }
        public override void PlayerOutsideZone(XRNetworkPlayer networkPlayer)
        {
            base.PlayerOutsideZone(networkPlayer);
            PopUpController.Instance.HidePopup(PopUpType.JoyridePopup);
        }
    }
}

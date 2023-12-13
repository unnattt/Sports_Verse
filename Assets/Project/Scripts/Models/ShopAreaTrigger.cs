using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using Yudiz.UI;

namespace Sportsverse
{
    public class ShopAreaTrigger : TriggerZone
    {
        public Transform shopUILocation;
        private void Start()
        {
            if(shopUILocation == null)
            {
                shopUILocation = transform;
            }
        }
        public override void PlayerInsideZone(XRNetworkPlayer networkPlayer)
        {
            base.PlayerInsideZone(networkPlayer);
            PopUpController.Instance.GetPopup<ShopPopup>(PopUpType.ShopPopup).MoveScreen(shopUILocation);
            PopUpController.Instance.ShowPopup(PopUpType.ShopPopup);
        }
        public override void PlayerOutsideZone(XRNetworkPlayer networkPlayer)
        {
            base.PlayerOutsideZone(networkPlayer);
            PopUpController.Instance.HidePopup(PopUpType.ShopPopup);
        }
    }
}
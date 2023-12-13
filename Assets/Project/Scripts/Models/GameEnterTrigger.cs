using Sportsverse.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yudiz.BaseFramework;
using Yudiz.UI;

namespace Sportsverse
{
    public class GameEnterTrigger : TriggerZone
    {
        [SerializeField] private PlayerInventoryData playerInventoryData;
        [SerializeField] private GameTypes gameType;
        [SerializeField] private string confirmationMessage;
        [SerializeField] private string alertMessage;
        public override void PlayerInsideZone(XRNetworkPlayer networkPlayer)
        {
            base.PlayerInsideZone(networkPlayer);
            //if (playerInventoryData.HasTicket(gameType))
            //{
                PopUpController.Instance.GetPopup<ConfirmationPopup>(PopUpType.ConfirmationPopup).SetConfirmationData(confirmationMessage, () =>
                {
                    //playerInventoryData.UseTicket(gameType);
                    AssetBundlesHandler.Instance.LoadGame(gameType);
                }, null);
                PopUpController.Instance.ShowPopup(PopUpType.ConfirmationPopup);
            //}
            //else
            //{
            //    Debug.Log("No Tickets For Game");
            //    PopUpController.Instance.GetPopup<ConfirmationPopup>(PopUpType.ConfirmationPopup).SetConfirmationData(alertMessage, () =>
            //    {
            //        PopUpController.Instance.HidePopup(PopUpType.ConfirmationPopup);
            //    });
            //    PopUpController.Instance.ShowPopup(PopUpType.ConfirmationPopup);
            //}
        }

        public override void PlayerOutsideZone(XRNetworkPlayer networkPlayer)
        {
            base.PlayerOutsideZone(networkPlayer);
            PopUpController.Instance.HidePopup(PopUpType.ConfirmationPopup);
        }
    }
}
using Sportsverse.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sportsverse
{
    public class GameManager : Yudiz.IndestructibleSingleton<GameManager>
    {
        public XRNetworkPlayer networkLocalPlayer;

        public PlayerInventoryData playerInventoryData;

        public override void Awake()
        {
            base.Awake();
            playerInventoryData.ClearData();
        }

        public void AssignLocalPlayer(XRNetworkPlayer player)
        {
            networkLocalPlayer = player;
        }
    }
}
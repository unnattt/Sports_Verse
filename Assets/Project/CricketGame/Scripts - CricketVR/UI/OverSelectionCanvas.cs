using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{
    public class OverSelectionCanvas : BaseScreen
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS

        [SerializeField] private int overs;

        [SerializeField] private Image overselectionImage;
        [SerializeField] private TextMeshProUGUI overText;
        [SerializeField] private Button nextBtn;
        [SerializeField] private Button increaseOvers;
        [SerializeField] private Button decreaseOvers;


        [SerializeField] private Transform canvasTransform;

        //const Strings
        private const string player1 = "Player 1";
        private const string player2 = "Player 2";
        #endregion

        #region UNITY_CALLBACKS

        private void Start()
        {
            nextBtn.onClick.AddListener(OnClickNextBtn);
            increaseOvers.onClick.AddListener(IncreaseAmount);
            decreaseOvers.onClick.AddListener(DecreaseAmount);
            SetOvers();

            Animate();
        }

        private void Animate()
        {
            AnimationManager.instance.ScaleTransform(canvasTransform, 1.5f, 0);
        }

        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS

        public void OnClickNextBtn()
        {
            StorePlayerData(player1);
            StorePlayerData(player2);

            GameEvents.updateGameData?.Invoke();
            OversManager.instance.SetOversForEachPlayer(overs);
            ScreenManager.instance.SwitchScreen(ScreenName.PlayerTurnScreen);
        }

        #endregion

        #region PRIVATE_FUNCTIONS


        private void StorePlayerData(string name)
        {
            ScoreManager.inst.SavePlayerData(name);
        }

        private void IncreaseAmount()
        {
            Debug.Log("Amount Increased");
            overs++;
            overText.text = overs.ToString();
        }

        private void DecreaseAmount()
        {
            if (overs > 1)
            {
                overs--;
                overText.text = overs.ToString();
            }
        }

        private void SetOvers()
        {
            overs = 1;
            overText.text = overs.ToString();
        }

        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
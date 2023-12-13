using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{
    public class PlayerStatsCanvas : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI currentPlayerName;
        [SerializeField] private TextMeshProUGUI totalRunsScored;
        [SerializeField] private TextMeshProUGUI totalBoundariesScored;
        [SerializeField] private TextMeshProUGUI totalFoursScored;
        [SerializeField] private TextMeshProUGUI totalSixesScored;
        [SerializeField] private TextMeshProUGUI runrates;
        [SerializeField] private Button nextButton;

        [SerializeField] private Transform canvasTransform;

        //Const Strings
        private const string performanceSummary = "'s Performance Summary";
        private const string totalRuns = "Total Runs : ";
        private const string totalFours = "Total Fours : ";
        private const string totalSixes = "Total Sixes : ";
        private const string totalBoundaries = "Total Boundaries : ";
        private const string runRates = "Run Rate : ";


        private void OnEnable()
        {
            GameEvents.setDataForPlayerStatsScreen += SetDataForPlayerStatsScreen;
        }

        private void OnDisable()
        {
            GameEvents.setDataForPlayerStatsScreen -= SetDataForPlayerStatsScreen;
        }

        private void Start()
        {
            nextButton.onClick.AddListener(NextScreen);
        }

        private void SetDataForPlayerStatsScreen()
        {
            currentPlayerName.text = ScoreManager.inst.currentplayerName + performanceSummary;
            totalRunsScored.text = totalRuns + ScoreManager.inst.currentPlayerScore;
            totalFoursScored.text = totalFours + ScoreManager.inst.currentTotalFours;
            totalSixesScored.text = totalSixes + ScoreManager.inst.currentTotalSixes;
            totalBoundariesScored.text = totalBoundaries + ScoreManager.inst.currentTotalBoundaries;
            runrates.text = runRates + OversManager.instance.runrate;


            //temp
            Animate();
        }

        private void Animate()
        {
            AnimationManager.instance.ScaleTransform(canvasTransform, 1.5f, 0);
        }

        public void NextScreen()
        {
            if (ScoreManager.inst.currentPlayerIndex < 1)
            {
                Debug.Log("Starting Next Player");
                GameManager.inst.StartNextPlayerTurn();

            }
            else
            {
                Debug.Log("PLayer Index is Greater");
                ScreenManager.instance.SwitchScreen(ScreenName.WinningScreen);
                GameEvents.onWinning?.Invoke();
            }
        }
    }

   

}
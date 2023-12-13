using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{
    public class ScoreBoardCanvas : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI currentPlayerName;
        [SerializeField] private TextMeshProUGUI currentPlayerScore;
        [SerializeField] private TextMeshProUGUI wicketsLeft;
        [SerializeField] private TextMeshProUGUI oversBallsLeft;

        //constant Strings
        private const string runsString = "Runs : ";
        private const string wicketsLeftString = "Wickets Left : ";
        private const string ballsLefttString = "Balls Left : ";

        private void OnEnable()
        {
            GameEvents.setDataForGamePlayScreen += SetGamePlayScreenData;
        }

        private void OnDisable()
        {
            GameEvents.setDataForGamePlayScreen -= SetGamePlayScreenData;
        }

        public void SetGamePlayScreenData()
        {
            Debug.Log("Gameplay screenData set");

            currentPlayerName.text = ScoreManager.inst.currentplayerName;

            if (ScoreManager.inst.currentPlayerIndex > 0)
            {
                currentPlayerScore.text = ScoreManager.inst.currentPlayerScore.ToString() + "/" + ScoreManager.inst.players[0].totalRuns;
            }
            else
            {
                currentPlayerScore.text = ScoreManager.inst.currentPlayerScore.ToString();
            }
            wicketsLeft.text = OversManager.instance.wicketsLost + "/2";
            oversBallsLeft.text = ScoreManager.inst.oversBall + "/"+ OversManager.instance.oversPerPlayer;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{

    public class WinnerCanvas : BaseScreen
    {
        [Header("WInner Player Text")]
        [SerializeField] private TextMeshProUGUI WinnerPlayerText;
        [SerializeField] private TextMeshProUGUI loserPlayerText;
        [SerializeField] private TextMeshProUGUI tieGameText;

        [SerializeField] private int player1Score;
        [SerializeField] private int player2Score;
        [SerializeField] private string player1Name;
        [SerializeField] private string player2Name;
        [SerializeField] private Button playAgainBtn;
        [SerializeField] private Button quitGame;

        [SerializeField] private Transform canvasTransform;

        //Constant Strings
        private const string winsString = " Wins ";
        private const string tieString = "Its a Tie ! Both Player Scored ";

        private void Start()
        {
            playAgainBtn.onClick.AddListener(PlayAgain);
            quitGame.onClick.AddListener(QuitGame);
        }

        private void OnEnable()
        {
            GameEvents.onWinning += ComparePlayerRuns;
        }

        private void OnDisable()
        {
            GameEvents.onWinning -= ComparePlayerRuns;
        }

        [ContextMenu("ComparePlayerRuns")]
        public void ComparePlayerRuns()
        {
            AudioManager.inst.PlayAudio(AudioManager.AudioName.OnWinning);

            player1Score = ScoreManager.inst.players[0].totalRuns;
            player2Score = ScoreManager.inst.players[1].totalRuns;
            player1Name = ScoreManager.inst.players[0].playerName;
            player2Name = ScoreManager.inst.players[1].playerName;

            if (player1Score > player2Score)
            {
                WinnerPlayerText.text = player1Name + " Wins with the score of " + "<b> <color=#FD5468>" + player1Score + "</color> </b>" + " Runs !";
                loserPlayerText.text =  player2Name + " Scored" + "<b> <color=#FD5468>" + player2Score + "</color> </b>" + " Runs";
                tieGameText.text = "";
            }
            else if (player1Score == player2Score)
            {
                tieGameText.text = tieString + "<b> <color=#FD5468>" + player2Score + "</color> </b>" + " Runs";
                WinnerPlayerText.text = "";
                loserPlayerText.text = "";
            }
            else
            {
                WinnerPlayerText.text = player2Name + " Wins with the score of " + "<b> <color=#FD5468>" + player2Score + "</color> </b>" + " Runs !";
                loserPlayerText.text =  player1Name + " Scored" + "<b> <color=#FD5468>" + player1Score + "</color> </b>" + " Runs";
                tieGameText.text = "";
            }

            Animate();
        }

        private void Animate()
        {
            AnimationManager.instance.ScaleTransform(canvasTransform, 1.5f, 0);
        }

        public void PlayAgain()
        {
            GameManager.inst.playerIndex = 0;

            ScoreManager.inst.players.Clear();
            ScoreManager.inst.currentPlayerIndex = 0;

            OversManager.instance.wicketsPerPlayer = 2;
            OversManager.instance.UpdatePlayerWickets();
            OversManager.instance.ResetData();

            ScreenManager.instance.SwitchScreen(ScreenName.OverSelectionScreen);
        }

        private void QuitGame()
        {
            //Application.Quit();
        }
    }
}


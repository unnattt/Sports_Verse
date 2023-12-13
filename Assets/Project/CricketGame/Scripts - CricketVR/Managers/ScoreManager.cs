using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Yudiz.VRCricket.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager inst;

        public List<Player> players = new List<Player>();

        [SerializeField] private GameObject popUpPrefab;

        [Space(10)]
        [Header("Current Player Stats")]
        public string currentplayerName;
        public int currentPlayerScore;
        public int currentWicketsLeft;
        public int currentTotalFours;
        public int currentTotalSixes;
        public int currentTotalBoundaries;
        public int currentPlayerIndex;
        public int currentOverLeft;
        public int currentPlayersBallLeft;
        public float currentPlayerRunrate;
        public string oversBall;

        private int singleRun = 1;
        private int doubleRuns = 2;
        private int tripleRuns = 3;

        private float popUpMinDistance = 15;
        private float destroyPopUpIn = 1;

        //const strings
        private const string oneRun = "+1";
        private const string twoRuns = "+2";
        private const string ThreeRuns = "+3";

        private void Awake()
        {
            inst = this;
        }

        private void OnEnable()
        {
            GameEvents.updateGameData += UpdatePlayerName;
            GameEvents.updateGameData += SetDataForPlayerTurnScreen;
        }

        private void OnDisable()
        {
            GameEvents.updateGameData -= UpdatePlayerName;
            GameEvents.updateGameData -= SetDataForPlayerTurnScreen;
        }


        public void SavePlayerData(string name)
        {
            Player playerData = new();

            playerData.playerName = name;
            players.Add(playerData);
        }


        #region CurrentPlayerDataUpdates
        public void AddScore(int scoreToAdd)
        {
            Debug.Log("Score Added");

            players[currentPlayerIndex].totalRuns += scoreToAdd;
            currentPlayerScore = players[currentPlayerIndex].totalRuns;
        }

        public void UpdatePlayerName()
        {
            currentplayerName = players[currentPlayerIndex].playerName;
        }

        public void UpdateOvers(int OversToUpdate)
        {
            currentOverLeft = OversToUpdate;
        }

        public void UpdateBalls(int BallToUpdate)
        {
            currentPlayersBallLeft = BallToUpdate;
        }

        public void UpdateCurrentPLayerIndex(int indexToSet)
        {
            Debug.Log("Scoremanager Player Index Changed");
            currentPlayerIndex = indexToSet;
        }

        public void UpdateCurrentWickets(int wicketsToUpdate)
        {
            currentWicketsLeft = wicketsToUpdate;
        }

        public void UpdateTotalFours(int totalFoursToset)
        {
            players[currentPlayerIndex].totalFours += totalFoursToset;
            currentTotalFours = players[currentPlayerIndex].totalFours;
        }

        public void UpdateTotalSixes(int totalSixesToSet)
        {
            players[currentPlayerIndex].totalSixes += totalSixesToSet;
            currentTotalSixes = players[currentPlayerIndex].totalSixes;
        }

        public void UpdateTotalBoundaries(int totalBoundariesToset)
        {
            players[currentPlayerIndex].totalBoundaries += totalBoundariesToset;
            currentTotalBoundaries = players[currentPlayerIndex].totalBoundaries;
        }

        public void UpdateRunRates(float runrateToUpdate)
        {
            players[currentPlayerIndex].runrate = runrateToUpdate;
            currentPlayerRunrate = players[currentPlayerIndex].runrate;
        }
        #endregion


        #region UpdatesCanvasData
        public void SetDataForGamePlayScreen()
        {
            GameEvents.setDataForGamePlayScreen?.Invoke();
            Debug.Log("Gameplay screen Data set");
        }

        public void SetDataForPlayerTurnScreen()
        {
            GameEvents.setDataForPlayerTurnScreen?.Invoke();
            Debug.Log("Players Turn Data Set");
        }

        public void SetDataForPlayerStatsScreen()
        {
            GameEvents.setDataForPlayerStatsScreen?.Invoke();
            Debug.Log("PlayerStats Data Set");
        }
        #endregion


        #region HandlesScores
        public void HandleNormalScores(float distance, Vector3 PopupPos, Vector3 playerPos)
        {
            if (distance < 20)
            {
                Debug.Log("1 score Added");

                HandlePopups(oneRun, PopupPos, playerPos);
                players[currentPlayerIndex].totalRuns += singleRun;
            }
            else if (distance >= 20 && distance <= 40)
            {
                Debug.Log("2 score Added");

                HandlePopups(twoRuns, PopupPos, playerPos);
                players[currentPlayerIndex].totalRuns += doubleRuns;
            }
            else if (distance >= 40 && distance <= 100)
            {
                Debug.Log("3 score Added");

                HandlePopups(ThreeRuns, PopupPos, playerPos);
                players[currentPlayerIndex].totalRuns += tripleRuns;
            }

            currentPlayerScore = players[currentPlayerIndex].totalRuns;
            SetDataForGamePlayScreen();
        }

        public void HandlePopups(string textForPopup, Vector3 PopUpPosition, Vector3 PlayerPositon)
        {
            GameObject popUp = Instantiate(popUpPrefab, PopUpPosition, Quaternion.identity);

            if (Vector3.Distance(PopUpPosition, PlayerPositon) < popUpMinDistance)
            {
                Debug.Log("PopUp Position Modified");
                popUp.transform.position = transform.position;
            }
            Vector3 dir = popUp.transform.position - Camera.main.transform.position;
            Vector3 lookatpoint = popUp.transform.position + dir;

            popUp.transform.LookAt(lookatpoint);
            popUp.GetComponent<TextMeshPro>().text = textForPopup;

            Destroy(popUp, destroyPopUpIn);
        }

        public void ResetScores()
        {
            currentPlayerScore = 0;
            currentTotalFours = 0;
            currentTotalSixes = 0;
            SetDataForGamePlayScreen();
        }
        #endregion
    }

    [System.Serializable]
    public class Player
    {
        public string playerName;
        public int totalRuns;
        public int totalBoundaries;
        public int totalSixes;
        public int totalFours;
        public float runrate;
    }

}
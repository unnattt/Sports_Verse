using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{
    public class HighScoresCanvas : MonoBehaviour
    {
        //[SerializeField] private List<PlayerData> playerList = new List<PlayerData>();
        [SerializeField] private List<TextMeshProUGUI> highScoresList = new List<TextMeshProUGUI>();
        [SerializeField] private List<TextMeshProUGUI> Runrates = new List<TextMeshProUGUI>();

        [SerializeField] private List<Player> playerDataList = new List<Player>();

        public int numberOfPlayers = 10;

        private List<string> playerNames = new List<string>()
        {
                "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Hannah", "Ivy", "Jack", "Katie", "Liam", "Mia", "Noah", "Olivia", "Sophia", "William", "Xavier", "Yasmine", "Zoe"
        };

        //private void OnEnable()
        //{
        //    GameEvents.calculateRunrates += AddPlayerData;
        //}

        //private void OnDisable()
        //{
        //    GameEvents.calculateRunrates -= AddPlayerData;
        //}

        //[ContextMenu("DeleteData")]
        //private void DeleteData()
        //{
        //    PlayerPrefs.DeleteAll();
        //    playerList.Clear();
        //    UpdateHighScoresUI();
        //}

        //[ContextMenu("AddDummyData")]
        //private void AddDummyData()
        //{
        //    AddPlayerData("Luffy", 32);
        //    AddPlayerData("Naruto", 25);
        //    AddPlayerData("Goku", 20);
        //}

        private void Start()
        {
            //LoadPlayerData();
            //UpdateHighScoresUI();
            GeneratePlayerData();
        }

        private void GeneratePlayerData()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                string playerName = GetRandomPlayerName();
                int score = Random.Range(20, 36);

                Player player = new Player(playerName, score);
                playerDataList.Add(player);
            }

            UpdateLeaderBoard();
        }

        private string GetRandomPlayerName()
        {
            int randomIndex = Random.Range(0, playerNames.Count);
            return playerNames[randomIndex];
        }

        private void UpdateLeaderBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                highScoresList[i].text = playerDataList[i].playerName;
                Runrates[i].text = playerDataList[i].score.ToString();
            }
        }

        //public void AddPlayerData(string playerName, float runRate)
        //{
        //    Debug.Log("New RunRate Added");
        //    //Debug.Log("Count in AddPlayer AT STart : " + playerList.Count);

        //    //PlayerData newPlayerData = new PlayerData();

        //    //newPlayerData.playerName = playerName;
        //    //newPlayerData.playerRunRate = runRate;

        //    //playerList.Add(newPlayerData);

        //    //playerList.Sort((a, b) => b.playerRunRate.CompareTo(a.playerRunRate));

        //    //if (playerList.Count > 3)
        //    //{
        //    //    playerList.RemoveAt(playerList.Count - 1);
        //    //}

        //    //SavePlayerData();
        //    //UpdateHighScoresUI();
        //    //Debug.Log("Count in AddPlayer at END : " + playerList.Count);
        //}


        //private void SavePlayerData()
        //{
        //    //Debug.Log("Count in SavePlayerData : " + playerList.Count);
        //    //for (int i = 0; i < playerList.Count; i++)
        //    //{
        //    //    Debug.Log("PLayer Data Saved");
        //    //    PlayerPrefs.SetString("PlayerName" + i, playerList[i].playerName);
        //    //    PlayerPrefs.SetFloat("PlayerRunrate" + i, playerList[i].playerRunRate);
        //    //}
        //}


        //private void LoadPlayerData()
        //{
        //    //playerList.Clear();
        //    //for (int i = 0; i < 3; i++)
        //    //{
        //    //    Debug.Log("PLayer Data Loaded");
        //    //    string playerName = PlayerPrefs.GetString("PlayerName" + i, "");
        //    //    float playerRunRate = PlayerPrefs.GetFloat("PlayerRunrate" + i, 0f);

        //    //    if (!string.IsNullOrEmpty(playerName) && playerRunRate > 0f)
        //    //    {
        //    //        PlayerData loadedPlayerData = new PlayerData();
        //    //        loadedPlayerData.playerName = playerName;
        //    //        loadedPlayerData.playerRunRate = playerRunRate;
        //    //        playerList.Add(loadedPlayerData);
        //    //    }
        //    //}
        //}

        //private void UpdateHighScoresUI()
        //{
        //    //for (int i = 0; i < highScoresList.Count; i++)
        //    //{
        //    //    if (i < playerList.Count)
        //    //    {
        //    //        Debug.Log("PLayer Data Updated");
        //    //        //highScoresList[i].text = playerList[i].playerName + ": " + playerList[i].playerRunRate.ToString("F2");
        //    //        highScoresList[i].text = playerList[i].playerName + ": ";
        //    //        Runrates[i].text = playerList[i].playerRunRate.ToString("F2");
        //    //    }
        //    //    else
        //    //    {
        //    //        highScoresList[i].text = "";
        //    //        Runrates[i].text = "";
        //    //    }
        //    //}
        //}
    }

    public class Player
    {
        public string playerName;
        public int score;

        public Player(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }

    //[System.Serializable]
    //public class PlayerData
    //{
    //    public string playerName;
    //    public float playerRunRate;
    //}
}


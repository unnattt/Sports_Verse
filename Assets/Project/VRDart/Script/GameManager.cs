using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Yudiz.VRDart.DartController;
using Yudiz.VRDart.Data;

namespace Yudiz.VRDart.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager _instance;

        public int initialDartCount;
        public int currentDartCount;
        public GameObject dartPrefab;
        public int Score_;
        public int scoreSystem;
        private int SelectedScore;
        public int TotalPlayerCount;
        public GameObject gameplayObj;
        public TMP_Text Displayerscore;
        public int PlayerCount;
        private string name;
        public TMP_Text DisplayPlayer;
        public GameObject PlayerNameItem;
        public GameObject listParent;
        public PlayerData currentPlayer;
        public List<GameObject> ThrowedDartList;
        public Transform initialPoint;
        private int index;
        private float farDistance = 1.5f;
        public List<int> Tempcount = new List<int>();
        public List<PlayerData> LeaderBoardData = new List<PlayerData>();
        public Vector3 currentDartPos;
        public GameObject XROrigin;
        public List<DartMovement> totalDart;
        public DartMovement currentDart;
        public GameObject LeaderboardLable;

        public void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _instance = this;
            currentDartCount = initialDartCount;


        }

        public void DartInstantiate()
        {

            for (int i = 0; i < initialDartCount; i++)
            {
                GameObject obj = Instantiate(dartPrefab, new Vector3(initialPoint.position.x + (i * 0.15f), initialPoint.position.y, initialPoint.position.z), initialPoint.rotation);
                totalDart.Add(obj.GetComponent<DartMovement>());
            }

        }

        public void PlayerInstantiate()
        {

            Debug.Log("Call");
            gameplayObj.SetActive(true);
            currentPlayer = Instantiate(PlayerNameItem, listParent.transform.position, listParent.transform.rotation, listParent.transform).GetComponent<PlayerData>();
            currentPlayer.gameObject.SetActive(false);
            if (Tempcount.Count != 0)
            {
                index = UnityEngine.Random.Range(0, Tempcount.Count);
                name = "Player_" + Tempcount[index];
                Tempcount.RemoveAt(index);
            }

            currentPlayer.playerName.text = name;
            Debug.Log("-----PlayerInstantiate_Score_----" + Score_);
            Debug.Log("-----PlayerInstantiate_scoreSystem----" + scoreSystem);
            Debug.Log("-----PlayerInstantiate_SelectedScore----" + SelectedScore);

            currentPlayer.SetScore(Score_);
            PlayerCount--;
            ScreenManager._instance.playerName.text = name;
            DisplayPlayer.text = name;
            ScreenManager._instance.animationScreen.SetActive(true);
            SoundManager.inst.PlayerNameSoundPlay(Enum.Parse<SoundManager.PlayerNameSound>(name));
        }


        public void GameType(int count)
        {

            for (int i = 0; i < PlayerCount; i++)
            {
                Tempcount.Add(i + 1);
            }
            if (count == 0)
            {
                Score_ = 301;
                scoreSystem = Score_;
                SelectedScore = Score_;
                Displayerscore.text = scoreSystem.ToString();
            }
            else if (count == 1)
            {
                Score_ = 501;
                scoreSystem = Score_;
                Displayerscore.text = scoreSystem.ToString();
            }
            ScreenManager._instance.gameSelectionScreen._gameSelectionScreen.enabled = false;
            SoundManager.inst.loopaudioSource.volume = 0.3f;
            PlayerInstantiate();

        }

        public void AddScore(int no)
        {
            scoreSystem -= no;
            //scoreSystem -= no;
            Displayerscore.text = scoreSystem.ToString();
        }

        public void LeaderboardList()
        {
            ScreenManager._instance.animationScreen.SetActive(true);
            PlayerInstantiate();

        }

        public async void TurnChangResetDart()
        {
            Debug.Log("-----TurnChangResetDart_Score----" + Score_);
            Debug.Log("-----TurnChangResetDart_scoreSystem----" + scoreSystem);
            Debug.Log("-----TurnChangResetDart_SelectedScore----" + SelectedScore);
            currentPlayer.SetScore(scoreSystem);
            currentDartCount = initialDartCount;
            currentPlayer.gameObject.SetActive(true);
            LeaderBoardData.Add(currentPlayer);
            if (LeaderBoardData.Count > 1)
            {
                for (int i = 0; i < LeaderBoardData.Count - 1; i++)
                {
                    for (int j = 0; j < LeaderBoardData.Count - i - 1; j++)
                    {
                        if (LeaderBoardData[j].score > LeaderBoardData[j + 1].score)
                        {
                            string Pname = LeaderBoardData[j].playerName.text;
                            int Pscore = LeaderBoardData[j].score;
                            LeaderBoardData[j].playerName.text = LeaderBoardData[j + 1].playerName.text;
                            LeaderBoardData[j].SetScore(LeaderBoardData[j + 1].score);
                            LeaderBoardData[j + 1].playerName.text = Pname;
                            LeaderBoardData[j + 1].SetScore(Pscore);
                        }

                    }
                }
            }

            if (PlayerCount != 0)
            {
                LeaderboardList();

            }
            else
            {
                Debug.Log("Game Over");
                ScreenManager._instance.gameOver.SetActive(true);
                ScreenManager._instance.gameOverScreen.winnerName.text = ScreenManager._instance.gameOverScreen.ListofPlayerFirst.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text;
                return;
            }
            scoreSystem = SelectedScore;
            Displayerscore.text = SelectedScore.ToString();
            for (int i = 0; i < ThrowedDartList.Count; i++)
            {
                Destroy(ThrowedDartList[i].gameObject);
            }

        }
        public async void NormalResetDart()
        {
            if (ScreenManager._instance.alartScreen._alartScreenCanvas.isActiveAndEnabled == false)
            {

                if (currentDart.Distance > farDistance)
                {

                    currentDartCount--;
                    currentDart.isdartthrow = false;
                    await Task.Delay(1000);
                    if (currentDartCount == 0)
                    {
                        TurnChangResetDart();
                    }
                    totalDart.Remove(currentDart);

                }
                else
                {
                    ThrowedDartList[ThrowedDartList.Count - 1].gameObject.transform.position = currentDartPos;
                    currentDart.isdartthrow = true;
                }

            }
            else
            {

                ThrowedDartList[ThrowedDartList.Count - 1].gameObject.transform.position = currentDartPos;
                currentDart.isdartthrow = true;
            }

            ActivetDartGreb();
            currentDart.dartRigidbody.isKinematic = true;
        }

        public void DisableDartGreb()
        {
            for (int i = 0; i < totalDart.Count; i++)
            {
                if (totalDart[i] != currentDart)
                {
                    totalDart[i].grabInteractable.enabled = false;
                }
            }
        }
        public void ActivetDartGreb()
        {
            Debug.Log("Activet");
            for (int i = 0; i < totalDart.Count; i++)
            {
                totalDart[i].grabInteractable.enabled = true;
            }
        }
        //[ContextMenu("PlayAgain")]
        public void PlayAgain()
        {
            ScreenManager._instance.gameOver.SetActive(false);
            for (int i = 0; i < LeaderBoardData.Count; i++)
            {
                Destroy(LeaderBoardData[i].gameObject);
            }
            for (int i = 0; i < totalDart.Count; i++)
            {
                Destroy(totalDart[i].gameObject);
            }
            Tempcount.Clear();
            scoreSystem = SelectedScore;
            totalDart.Clear();
            LeaderBoardData.Clear();
            Displayerscore.text = SelectedScore.ToString();
            PlayerCount = TotalPlayerCount;
            for (int i = 0; i < ThrowedDartList.Count; i++)
            {
                Destroy(ThrowedDartList[i].gameObject);
            }
            for (int i = 0; i < PlayerCount; i++)
            {
                Tempcount.Add(i + 1);
            }
            ThrowedDartList.Clear();
            PlayerInstantiate();
        }

        public void BackToMainMenu()
        {
            ScreenManager._instance.gameOver.SetActive(false);
            ScreenManager._instance.menuScreen.Show();
            for (int i = 0; i < LeaderBoardData.Count; i++)
            {
                Destroy(LeaderBoardData[i].gameObject);
            }
            for (int i = 0; i < totalDart.Count; i++)
            {
                if (totalDart[i] != null)
                    Destroy(totalDart[i].gameObject);
            }
            Tempcount.Clear();
            scoreSystem = SelectedScore;
            totalDart.Clear();
            LeaderBoardData.Clear();
            for (int i = 0; i < ThrowedDartList.Count; i++)
            {
                Destroy(ThrowedDartList[i].gameObject);
            }
            for (int i = 0; i < PlayerCount; i++)
            {
                Tempcount.Add(i + 1);
            }
            ThrowedDartList.Clear();
            gameplayObj.SetActive(false);
        }

    }
}



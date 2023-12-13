using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace Yudiz.VRBowling
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        public StoreScoreData scoreData;
        public int Score;
        public int tempHighScore;
        public int tempCurrentScore;
        static string filePath;
        public string MessageText = "Strick !";

        public Transform pointPos;
        public GameObject pointObj;

        public void Awake()
        {
            instance = this;
            filePath = Application.persistentDataPath + "/VrBollwing.fun";
            LoadData();
        }
        public void RoundwiseScore(int round)
        {
            switch (round)
            {
                case 1:

                    if (GameManager.instance.tempTurnCount == 1)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R1ChanceOne.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;


                    }
                    else if (GameManager.instance.tempTurnCount == 0)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R1ChanceTwo.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    UIManager.instance._ScoreSytemCanvas.FinalScore.text = Score.ToString();
                    //GameManager.instance.hitPinCount = 0;

                    break;
                case 2:
                    if (GameManager.instance.tempTurnCount == 1)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R2ChanceOne.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    else if (GameManager.instance.tempTurnCount == 0)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R2ChanceTwo.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }

                    UIManager.instance._ScoreSytemCanvas.FinalScore.text = Score.ToString();
                    //GameManager.instance.hitPinCount = 0;
                    break;
                case 3:
                    if (GameManager.instance.tempTurnCount == 1)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R3ChanceOne.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    else if (GameManager.instance.tempTurnCount == 0)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R3ChanceTwo.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }

                    UIManager.instance._ScoreSytemCanvas.FinalScore.text = Score.ToString();
                    //GameManager.instance.hitPinCount = 0;

                    break;
                case 4:
                    if (GameManager.instance.tempTurnCount == 1)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R4ChanceOne.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    else if (GameManager.instance.tempTurnCount == 0)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R4ChanceTwo.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    UIManager.instance._ScoreSytemCanvas.FinalScore.text = Score.ToString();
                    //GameManager.instance.hitPinCount = 0;
                    break;
                case 5:
                    if (GameManager.instance.tempTurnCount == 1)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R5ChanceOne.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    else if (GameManager.instance.tempTurnCount == 0)
                    {
                        //ShowMessageSound();
                        UIManager.instance._ScoreSytemCanvas.R5ChanceTwo.text = GameManager.instance.hitPinCount.ToString();
                        Score += GameManager.instance.hitPinCount;

                    }
                    UIManager.instance._ScoreSytemCanvas.FinalScore.text = Score.ToString();
                    //GameManager.instance.hitPinCount = 0;
                    break;

                default:
                    Debug.Log("Score------" + Score);
                    Debug.Log("TempScore------" + tempCurrentScore);
                    Score += GameManager.instance.hitPinCount;
                    UIManager.instance._ScoreSytemCanvas.FinalScore.text = Score.ToString();
                    tempCurrentScore = Convert.ToInt32(UIManager.instance._ScoreSytemCanvas.FinalScore.text);
                    break;
            }
        }

        public void ShowGainPoint(string Message)
        {
            GameObject point = Instantiate(pointObj, pointPos.position, Quaternion.identity, pointPos);
            TextMeshPro point_text = point.GetComponent<TextMeshPro>();
            point_text.text = MessageText;
            point_text.fontSize = 5;
            point_text.alignment = TextAlignmentOptions.Center;
            StartCoroutine(ShowGainPointAnimation(point_text, 1f));
        }

        private IEnumerator ShowGainPointAnimation(TextMeshPro text, float duration)
        {
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                text.transform.Translate(Vector3.up * Time.deltaTime);
                text.alpha = Mathf.Lerp(1, 0, time / duration);
                yield return null;
            }
            text.alpha = 0;
            yield return new WaitForSeconds(0.5f);
            Destroy(text.gameObject);
        }

        public void ShowMessageSound()
        {
            if (GameManager.instance.hitPinCount == 10)
            {
                SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.Sticke);
                ShowGainPoint(MessageText);
            }
            else if (GameManager.instance.hitPinCount == 0)
            {
                SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.MissShort);
            }
        }


        public void ResetScore()
        {
            UIManager.instance._ScoreSytemCanvas.R1ChanceOne.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R1ChanceTwo.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R2ChanceOne.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R2ChanceTwo.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R3ChanceOne.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R3ChanceTwo.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R4ChanceOne.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R4ChanceTwo.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R5ChanceOne.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.R5ChanceTwo.text = string.Empty;
            UIManager.instance._ScoreSytemCanvas.FinalScore.text = string.Empty;
            Score = 0; ;

        }


        public void SaveData()
        {
            string jsonData = JsonUtility.ToJson(scoreData);
            File.WriteAllText(filePath, jsonData);

        }

        public void LoadData()
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                scoreData = JsonUtility.FromJson<StoreScoreData>(jsonData);
                tempHighScore = Convert.ToInt32(scoreData.HighScore);
                //BinaryFormatter formatter = new BinaryFormatter();
                //FileStream stream = new FileStream(filePath, FileMode.Open);
                //appData = formatter.Deserialize(stream) as AppData;
                //stream.Close();
            }
            else
            {
                Debug.Log("No file Found");
            }

        }
        [Serializable]
        public class StoreScoreData
        {
            public float HighScore;
        }
    }
}
       
    

  







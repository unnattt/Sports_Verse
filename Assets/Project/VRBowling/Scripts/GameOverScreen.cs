using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Yudiz.VRBowling
{
    public class GameOverScreen : MonoBehaviour
    {
        public Canvas gameOverCanvas;
        private float uiOffset = 0.5f;
        public TMP_Text highScore;
        public TMP_Text yourScore;



        public void Home()
        {
            SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
            //SceneManager.LoadScene(0);
            GameManager.instance.Home();
        }
        public void OnEnable()
        {
            GameManager.OnGameOverScore += Score;
            SetCanvasPosition();


        }

        private async void SetCanvasPosition()
        {
            await Task.Delay(500);
            Vector3 uiDirection = Camera.main.transform.forward;
            uiDirection.y = 0;
            Vector3 newPosition = Camera.main.transform.position + uiDirection * uiOffset;

            //transform.position = Camera.main.transform.position + offset; 
            //transform.position = Vector3.Lerp(transform.position, newPosition,Time.deltaTime * alignmentSpeed);
            transform.position = newPosition;
            Vector3 direction = transform.position - Camera.main.transform.position;
            Vector3 lookAtPoint = transform.position + direction;
            transform.LookAt(lookAtPoint);
        }

        public void Score()
        {
            Debug.Log("--------------------");
            ScoreManager.instance.LoadData();
            yourScore.text = UIManager.instance._ScoreSytemCanvas.FinalScore.text;
            highScore.text = ScoreManager.instance.tempHighScore.ToString();
            ScoreManager.instance.tempCurrentScore = Convert.ToInt32(yourScore.text);


            //ScoreManager.instance.CheckPlayerHighScore(this);


            if (ScoreManager.instance.tempCurrentScore > ScoreManager.instance.tempHighScore)
            {
                ScoreManager.instance.scoreData.HighScore = Convert.ToInt32(yourScore.text);
                highScore.text = yourScore.text;
                //AudioManager.inst.PlayAudio(AudioManager.AudioName.Wooh);
            }
            else
            {
                //AudioManager.inst.PlayAudio(AudioManager.AudioName.BetterLuckNextTime);
                ScoreManager.instance.scoreData.HighScore = Convert.ToInt32(highScore.text);
            }
            ScoreManager.instance.SaveData();
        }

        public void OnDisable()
        {
            GameManager.OnGameOverScore -= Score;
        }

    }
}

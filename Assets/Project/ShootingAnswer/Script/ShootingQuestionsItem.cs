using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yudiz.ShootingGame.Manager;
using Yudiz.ShootingGame.GameEvents;
using Yudiz.ShootingGame.Utilities;

namespace Yudiz.ShootingGame.UI
{
    public class ShootingQuestionsItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        public TMPro.TMP_Text optionsText;
        public int winIndex;
        public int optionsIndex;

        public int currentQCount;

        public ShootingQuestionsScreen questionsScreen;
        //Canvas questionsScreenCanvas;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region PUBLIC_METHODS
        public void SetData(string optionsTxt, int winI, int optionsI, int count)
        {
            optionsText.text = optionsTxt;
            winIndex = winI;
            optionsIndex = optionsI;
            currentQCount = count;
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Method Triggered");
            if (other.CompareTag("Bullet"))
            {
                Debug.Log("Bullet Triggered");
                if (optionsIndex == winIndex)
                {
                    GoalDecisionCollider.optionAnswer = true;
                    gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                    ScoreManager.Instance.AddScore(1);
                }
                else
                {
                    GoalDecisionCollider.optionAnswer = false;
                    gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                }
                //Time.timeScale = 1;
                //await System.Threading.Tasks.Task.Delay(500);
                Debug.Log("Current Count " + currentQCount);
                //if (currentQCount < ShootingGameManager.Instance.totalQuestionCount && GameStateManager.Instance.currentGameState == GameStates.Playing)
                //{
                //    UiManager.Instance.ShowNextScreen(ScreenNames.QuestionScreen, 1500);
                //}
                if (ShootingGameManager.Instance.questionDatabase.questions.Length <= ShootingGameManager.Instance.questionCount)
                {
                    Events.onGameOver?.Invoke();
                    UiManager.Instance.ShowNextScreen(ScreenNames.GameOverScreen);
                    GameStateManager.Instance.ChangeGameState(GameStates.GameOver);
                    //UISystem.ViewController.Instance.ChangeView(UISystem.ScreenName.GameOverScreen);
                    //Debug.Log("GAME OVER" + GameManager.instance.questionDatabase.questions.Length + "-----" + GameManager.instance.questionCont);
                    ShootingGameManager.Instance.questionCount = 0;
                }
                else
                {
                    UiManager.Instance.ShowNextScreen(ScreenNames.QuestionScreen, 1500);
                }
                
                //UISystem.ViewController.Instance.HidePopup(UISystem.PopupName.QuestionsScreen);
            }
        }

        //public async void Click()
        //{
        //    if (optionsIndex == winIndex)
        //    {
        //        GoalDecisionCollider.optionAnswer = true;
        //        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        //    }
        //    else
        //    {
        //        GoalDecisionCollider.optionAnswer = false;
        //        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        //    }
        //    Time.timeScale = 1;
        //    await System.Threading.Tasks.Task.Delay(500);
        //    if (GameManager.instance.questionDatabase.questions.Length <= GameManager.instance.questionCont)
        //    {
        //        Events.onGameOver?.Invoke();
        //        UISystem.ViewController.Instance.ChangeView(UISystem.ScreenName.GameOverScreen);
        //        Debug.Log("GAME OVER" + GameManager.instance.questionDatabase.questions.Length + "-----" + GameManager.instance.questionCont);
        //        GameManager.instance.questionCont = 0;
        //    }
        //    UISystem.ViewController.Instance.HidePopup(UISystem.PopupName.QuestionsScreen);
        //}
        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region BASE_UI_CALLBACKS

        #endregion
    }
}


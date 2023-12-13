using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.VRBasketBall.Core;

namespace Yudiz.VRBasketBall.UI
{
    public class QuestionsItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        public TMPro.TMP_Text optionsText;
        public int winIndex;
        public int optionsIndex;
        //Canvas questionsScreenCanvas;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region PUBLIC_METHODS
        public void SetData(string optionsTxt,int winI,int optionsI)
        {
            optionsText.text = optionsTxt;
            winIndex = winI;
            optionsIndex=optionsI;
        }
        public async void Click()
        {
            if (optionsIndex == winIndex)
            {
                GoalDecisionCollider.optionAnswer = true;
                gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            }
            else
            {
                GoalDecisionCollider.optionAnswer = false;
                gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }
            Time.timeScale = 1;
            await System.Threading.Tasks.Task.Delay(500);
            if (GameManager.instance.questionDatabase.questions.Length <= GameManager.instance.questionCont)
            {
                Events.onGameOver?.Invoke();
                UISystem.ViewController.Instance.ChangeView(UISystem.ScreenName.GameOverScreen);
                Debug.Log("GAME OVER"+ GameManager.instance.questionDatabase.questions.Length+"-----"+ GameManager.instance.questionCont);
                GameManager.instance.questionCont = 0;
            }
            UISystem.ViewController.Instance.HidePopup(UISystem.PopupName.QuestionsScreen);
        }
        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region BASE_UI_CALLBACKS

        #endregion
    }
}


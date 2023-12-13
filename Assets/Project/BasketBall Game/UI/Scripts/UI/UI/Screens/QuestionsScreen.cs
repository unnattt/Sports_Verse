using UnityEngine;
using TMPro;
using Yudiz.VRBasketBall.Core;
using DG.Tweening;

namespace Yudiz.VRBasketBall.UI
{
	public class QuestionsScreen : UISystem.Popup
    {
        #region PUBLIC_VARS
        //public QuestionDatabase questionDatabase;
        public QuestionsItem questionsItem;
        public GameObject questionsItemParent;
        public TMP_Text questionsTxt;
        //public int questionsCount=0;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void OnEnable()
        {
            Events.onGameOver += QuetionsDataReset;
        }
        private void OnDisable()
        {
            Events.onGameOver -= QuetionsDataReset;
        }
        #endregion

        #region PUBLIC_METHODS
        public void QuestionSet()
        {
            int qCount = GameManager.instance.questionCont;
            QuestionDatabase database = GameManager.instance.questionDatabase;
            if (database.questions.Length != 0 && qCount < database.questions.Length)
            {
                OptionsClear();
                questionsTxt.text = database.questions[qCount].questionText;
                for (int i = 0; i < database.questions[qCount].options.Length; i++)
                {
                    QuestionsItem item = Instantiate(questionsItem, questionsItemParent.transform);
                    item.SetData(database.questions[qCount].options[i], database.questions[qCount].correctOptionIndex,i);
                }
                GameManager.instance.questionCont++;
            }
            else
            {
                Debug.Log("Question Database not assigned !"+"-----"+ database.questions.Length+"------"+ qCount);
            }
            

        }
		#endregion

		#region PRIVATE_METHODS
        private void OptionsClear()
        {
            if (questionsItemParent.transform.childCount>0)
            {
                foreach (Transform child in questionsItemParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        private void QuetionsDataReset()
        {
            OptionsClear();
            //GameManager.instance.questionCont=0;
        }
        #endregion

        #region BASE_UI_CALLBACKS

        public override void Show()
        {
            QuestionSet();
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
        }
        public override void Disable()
        {
            base.Disable();
        }
        public override void Redraw()
        {
            base.Redraw();
        }
       

        #endregion
    }
}


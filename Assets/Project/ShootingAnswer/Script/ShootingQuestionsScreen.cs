using UnityEngine;
using TMPro;
using Yudiz.ShootingGame.GameEvents;
using DG.Tweening;
using Yudiz.ShootingGame.Manager;

namespace Yudiz.ShootingGame.UI
{
    public class ShootingQuestionsScreen : UISystem.BaseUI
    {
        #region PUBLIC_VARS
        public QuestionDatabase questionDatabase;
        public ShootingQuestionsItem questionsItem;
        public GameObject questionsItemParent;
        public TMP_Text questionsTxt;

        private float canvasScale = 0.075f;
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
        [ContextMenu("NextQuetions")]
        public void QuestionSet()
        {
            int qCount = ShootingGameManager.Instance.questionCount;
            QuestionDatabase database = ShootingGameManager.Instance.questionDatabase;
            if (database.questions.Length != 0 && qCount < database.questions.Length)
            {
                OptionsClear();
                questionsTxt.text = database.questions[qCount].questionText;
                for (int i = 0; i < database.questions[qCount].options.Length; i++)
                {
                    ShootingQuestionsItem item = Instantiate(questionsItem, questionsItemParent.transform);
                    item.SetData(database.questions[qCount].options[i], database.questions[qCount].correctOptionIndex, i, qCount);
                }
                ShootingGameManager.Instance.questionCount++;
            }
            else
            {
                //GameStateManager.Instance.ChangeGameState(Utilities.GameStates.GameOver);
                //UiManager.Instance.ShowNextScreen(Utilities.ScreenNames.GameOverScreen);
                Debug.Log("Question Database not assigned !" + "-----" + database.questions.Length + "------" + qCount);
            }


        }
        #endregion

        #region PRIVATE_METHODS
        private void OptionsClear()
        {
            if (questionsItemParent.transform.childCount > 0)
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
            ShootingGameManager.Instance.questionCount = 0;
            //GameManager.instance.questionCont=0;
        }
        #endregion

        #region BASE_UI_CALLBACKS

        public override void Show()
        {
            //if (GameStateManager.Instance.currentGameState == Utilities.GameStates.Playing)
            {
                QuestionSet();
                base.Show();
                transform.localScale = Vector3.zero;
                transform.DOScale(canvasScale, 0.5f)
                .SetEase(Ease.OutBounce) // Use OutBounce easing for the bouncy effect
                .SetUpdate(true) // Ensure that the tween updates even when Time.timeScale is 0
                .SetRelative(false) // Use absolute values for scaling
                .SetEase(Ease.OutBounce) // Use bounce easing
                .SetLoops(1); // Set the number of loops (1 for no looping
            }
        }
        public override void Hide()
        {
            transform.DOScale(0, .075f)
            .SetEase(Ease.InBounce) // Use OutBounce easing for the bouncy effect
            .SetUpdate(true) // Ensure that the tween updates even when Time.timeScale is 0
            .SetRelative(false) // Use absolute values for scaling
            .SetEase(Ease.InBounce) // Use bounce easing
            .SetLoops(1); // Set th
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


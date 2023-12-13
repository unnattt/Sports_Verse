using System.Collections.Generic;
using UnityEngine;
using Yudiz.ShootingGame.Manager;

namespace Yudiz.ShootingGame.UI
{
    public class ChooseQuestionsCatogoryScreen : UISystem.Screen
    {
        [SerializeField] QuestionDatabase firstCatogorySo;
        [SerializeField] QuestionDatabase secondCatogorySo;
        [SerializeField] QuestionDatabase thirdCatogorySo;

        [SerializeField] List<Collider> childCollidersList;

        public void FirstCatogory()
        {
            Debug.Log("Button Clicked");
            ShootingGameManager.Instance.questionDatabase = firstCatogorySo;
            //Hide();
            UiManager.Instance.ShowNextScreen(Utilities.ScreenNames.QuestionScreen, 1000);
        }
        public void SecondCatogory()
        {
            ShootingGameManager.Instance.questionDatabase = secondCatogorySo;
            UiManager.Instance.ShowNextScreen(Utilities.ScreenNames.QuestionScreen, 1000);
        }
        public void ThirdCatogory()
        {
            ShootingGameManager.Instance.questionDatabase = thirdCatogorySo;
            //Hide();
            UiManager.Instance.ShowNextScreen(Utilities.ScreenNames.QuestionScreen, 1000);
        }

        private void ChildCollidersEnableDisable(bool value)
        {
            for (int i = 0; i < childCollidersList.Count; i++)
            {
                childCollidersList[i].enabled = value;
            }
        }

        #region BASE_UI_CALLBACKS

        public override void Show()
        {
            if (GameStateManager.Instance.currentGameState == Utilities.GameStates.ChooseQuestionCategory)
            {
                ChildCollidersEnableDisable(true);
                base.Show();
            }
        }
        public override void Hide()
        {
            Debug.Log("Hide Called");
            ChildCollidersEnableDisable(false);
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
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Yudiz.ShootingGame.Manager;
using Yudiz.ShootingGame.GameEvents;
using System.Collections.Generic;

namespace Yudiz.ShootingGame.UI
{
    public class GameOverScreen : UISystem.Screen
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] TMP_Text scoreTxt;
        [SerializeField] TMP_Text highScoreTxt;
        [SerializeField] Button playAgainBtn;
        [SerializeField] Button exitGameBtn;      

        [SerializeField] List<Collider> childColliders;
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            playAgainBtn.onClick.AddListener(OnPlayAgainBtnClick);
            exitGameBtn.onClick.AddListener(OnExitGameBtnClick);
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void SetUiData()
        {
            scoreTxt.text = ScoreManager.Instance.score.ToString();
            highScoreTxt.text = ScoreManager.Instance.highScore.ToString();
        }

        private void OnPlayAgainBtnClick()
        {
            Debug.Log("Replay Clicked");
            Events.onGameOver?.Invoke();
            UiManager.Instance.ShowNextScreen(Utilities.ScreenNames.QuestionScreen);
            GameStateManager.Instance.ChangeGameState(Utilities.GameStates.Playing);
        }

        private void OnExitGameBtnClick()
        {
            Application.Quit();
        }

        private void EnableDisableChildColliders(bool value)
        {
            for (int i = 0; i < childColliders.Count; i++)
            {
                childColliders[i].enabled = value;
            }
        }
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        public override void Show()
        {
            base.Show();
            SetUiData();
            EnableDisableChildColliders(true);
        }

        public override void Hide()
        {
            EnableDisableChildColliders(false);
            base.Hide();
        }
        #endregion
    }
}
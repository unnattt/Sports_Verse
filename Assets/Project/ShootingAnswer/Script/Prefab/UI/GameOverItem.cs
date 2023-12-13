using UnityEngine;
using UnityEngine.UI;
using Yudiz.BaseFramework;
using Yudiz.ShootingGame.GameEvents;
using Yudiz.ShootingGame.Manager;
using Yudiz.ShootingGame.Utilities;

namespace Yudiz.ShootingGame.Prefabs
{
    public class GameOverItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        [SerializeField] Image currentImg;
        [SerializeField] Sprite oldSprite;
        [SerializeField] Sprite newSprite;

        [SerializeField] int btnNo;
        // Btn no 1 => replay, Btn no 2 => Exit
        #endregion

        #region PRIVATE_VARS
        private void OnEnable()
        {
            currentImg.sprite = oldSprite;
        }
        private void OnDisable()
        {
            currentImg.sprite = oldSprite;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                currentImg.sprite = newSprite;
                if(btnNo == 1)
                {
                    Events.onGameOver?.Invoke();
                    ScoreManager.Instance.ResetScore();
                    UiManager.Instance.ShowNextScreen(ScreenNames.QuestionScreen);
                    GameStateManager.Instance.ChangeGameState(GameStates.Playing);
                }
                else
                {
                    //Application.Quit();
                    SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainScene, null);
                }
                other.gameObject.GetComponent<BulletItem>().BulletAutoDestroy();
            }
        }
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
using UnityEngine;
using UnityEngine.UI;
using Yudiz.ShootingGame.Manager;
using Yudiz.ShootingGame.UI;
using Yudiz.ShootingGame.Utilities;

namespace Yudiz.ShootingGame.Prefabs
{
    public class CategroyUIITem : MonoBehaviour
    {
        #region PUBLIC_VARS
        public Yudiz.ShootingGame.UI.ChooseQuestionsCatogoryScreen categoryScreen;
        public CategoryTypes category;
        public Image itemImg;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void OnEnable()
        {
            ResetColor();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                switch (category)
                {
                    case CategoryTypes.FirstCategory:
                        ChangeSpriteColor();
                        categoryScreen.FirstCatogory();
                        Debug.Log("First Cat");
                        break;

                    case CategoryTypes.SecondCategory:
                        ChangeSpriteColor();
                        categoryScreen.SecondCatogory();
                        Debug.Log("Second Cat");
                        break;

                    case CategoryTypes.ThirdCategory:
                        ChangeSpriteColor();
                        categoryScreen.ThirdCatogory();
                        Debug.Log("Third Cat");
                        break;
                }
                ShootingGameManager.Instance.SetTotalQuestionCount();
                GameStateManager.Instance.ChangeGameState(GameStates.Playing);
            }
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void ChangeSpriteColor()
        {
            itemImg.color = new Color32(0, 255, 97, 96);
        }

        private void ResetColor()
        {
            itemImg.color = new Color32(0, 0, 0, 96);
        }
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
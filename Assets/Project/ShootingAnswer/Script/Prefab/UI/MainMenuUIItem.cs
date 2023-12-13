using UnityEngine;
using UnityEngine.UI;

using Yudiz.ShootingGame.Manager;
using Yudiz.ShootingGame.Utilities;

namespace Yudiz.ShootingGame.Prefabs
{
    public class MainMenuUIItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        public Image currentImg;
        public Sprite oldSprite;
        public Sprite newSprite;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void OnEnable()
        {
            currentImg.sprite = oldSprite;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                currentImg.sprite = newSprite;
                UiManager.Instance.ShowNextScreen(ScreenNames.ChooseQuestionCategory, 1500);
                other.gameObject.GetComponent<BulletItem>().BulletAutoDestroy();
            }
        }
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
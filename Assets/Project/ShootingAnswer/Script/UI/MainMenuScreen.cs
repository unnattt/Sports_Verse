using UnityEngine;

using Yudiz.ShootingGame.Base;

namespace Yudiz.ShootingGame.UI
{
    public class MainMenuScreen : UISystem.Screen
    {
        #region PUBLIC_VARS
        public Collider childCollider;
        #endregion

        #region PRIVATE_VARS
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
        public override void Show()
        {
            childCollider.enabled = true;
            base.Show();
        }

        public override void Hide()
        {
            childCollider.enabled = false;
            base.Hide();
        }
        #endregion
    }
}
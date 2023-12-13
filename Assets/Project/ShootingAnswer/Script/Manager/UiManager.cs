using System.Collections.Generic;
using UISystem;
using UnityEngine;

using Yudiz.ShootingGame.Base;
using Yudiz.ShootingGame.Utilities;

namespace Yudiz.ShootingGame.Manager
{
    public class UiManager : Singleton<UiManager>
    {
        #region PUBLIC_VARS
        public List<ScreenType> screenTypes;
        public ScreenNames currentScreen;
        public ScreenNames InitScreen;
        #endregion

        #region PRIVATE_VARS                
        #endregion

        #region UNITY_CALLBACKS
        public override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            InitializeScreen();
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public async void ShowNextScreen(ScreenNames screen, int time = 0)
        {
            await System.Threading.Tasks.Task.Delay(time);
            screenTypes.Find(a => a.screenName == currentScreen).screenBase.Hide();
            currentScreen = screen;
            screenTypes.Find(a => a.screenName == currentScreen).screenBase.Show();
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        void InitializeScreen()
        {
            foreach (var s in screenTypes)
            {
                if (s.screenName == InitScreen)
                {
                    s.screenBase.Show();
                    currentScreen = s.screenName;
                }
                else
                {
                    s.screenBase.Hide();
                }
            }
        }
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
    [System.Serializable]
    public class ScreenType
    {
        public ScreenNames screenName;
        public BaseUI screenBase;
    }
}
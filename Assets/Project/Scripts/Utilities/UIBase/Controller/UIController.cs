using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yudiz.BaseFramework
{
    [Serializable]
    public class UIScreen
    {
        public ScreenType screenType;
        public UIScreenView screenView;
    }

    public enum ScreenType
    {
        None,
        SplashScreen, 
        LauncherScreen
    }
    public class UIController : Singleton<UIController>
    {

        public ScreenType StartScreen;
        public List<UIScreen> Screens;
        public string screenName;
        [SerializeField]
        public List<ScreenType> currentScreens;



        private IEnumerator Start()
        {
            currentScreens = new List<ScreenType>();

            yield return null;
            ShowScreen(StartScreen);

            yield return new WaitForSeconds(1f);

        }

        public void ShowNextScreen(ScreenType screenType, float Delay = 0.2f)
        {
            if (currentScreens.Count > 0)
            {
                HideScreen(currentScreens.Last());
            }
            else
            {
                Delay = 0;
            }

            StartCoroutine(ExecuteAfterDelay(Delay, () =>
            {
                ShowScreen(screenType);
            }));

        }

        public void ShowScreen(ScreenType screenType)
        {
            getScreen(screenType).Show();

            currentScreens.Add(screenType);
        }

        public void HideScreen(ScreenType screenType)
        {
            getScreen(screenType).Hide();

            currentScreens.Remove(screenType);
        }

        public UIScreenView getScreen(ScreenType screenType)
        {
            return Screens.Find(screen => screen.screenType == screenType).screenView;
        }

        IEnumerator ExecuteAfterDelay(float Delay, Action CallbackAction)
        {
            yield return new WaitForSeconds(Delay);

            CallbackAction();
        }
        public void OnSidePanelTap()
        {

        }
    }
}
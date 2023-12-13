using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Yudiz.VRCricket.UI
{


    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager instance;
        public BaseScreen[] screens;
        public BaseScreen currentScreen;
        public ScreenName previouScreen;


        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            currentScreen.ShowScreen();
        }

        public void SwitchScreen(ScreenName screentype)
        {
            previouScreen = currentScreen.Screen;

            if (currentScreen != null)
            {
                currentScreen.HideScreen();
            }
            foreach (BaseScreen baseScreen in screens)
            {
                if (baseScreen.Screen == screentype)
                {
                    baseScreen.ShowScreen();
                    currentScreen = baseScreen;

                    break;
                }
            }
        }

    }
}
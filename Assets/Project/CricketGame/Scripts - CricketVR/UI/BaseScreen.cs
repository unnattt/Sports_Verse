using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRCricket.UI
{


    public class BaseScreen : MonoBehaviour
    {
        public ScreenName Screen;
        public Canvas canvas;

        private void Awake()
        {
            HideScreen();
        }

        public virtual void ShowScreen()
        {
            canvas.enabled = true;
        }

        public virtual void HideScreen()
        {
            canvas.enabled = false;
        }
    }

    public enum ScreenName
    {
        MainScreen,
        OverSelectionScreen,
        PlayerTurnScreen,
        GameInfoTextScreen,
        PlayerStatsScreen,
        EndGameScreen,
        WinningScreen,
        countDownScreen,
        StartScreen
        
    }
}
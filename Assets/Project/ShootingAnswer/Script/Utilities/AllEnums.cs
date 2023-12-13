using UnityEngine;

namespace Yudiz.ShootingGame.Utilities
{
    public enum ScreenNames
    {
        MainMenu,       //Start Btn Screen
        ChooseQuestionCategory,   //Select Question Category Screen
        QuestionScreen,         //Questions Screen
        GameOverScreen,     //Game Over Screen
    }

    public enum CategoryTypes
    {
        FirstCategory,
        SecondCategory,
        ThirdCategory,
    }

    public enum GameStates
    {
        ChooseQuestionCategory,
        Playing,
        GameOver,
    }
}
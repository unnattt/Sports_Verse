using System;
using UnityEngine;

using Yudiz.ShootingGame.Base;

namespace Yudiz.ShootingGame.Manager
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        #region PUBLIC_VARS
        public int score;

        public int highScore;

        public Action<int> ShowScore;
        #endregion

        #region PRIVATE_VARS        
        #endregion

        #region UNITY_CALLBACKS
        public override void Awake()
        {
            base.Awake();
        }        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void AddScore(int points)
        {
            score += points;
            ShowScore?.Invoke(score);
            if(score > highScore)
            {
                highScore = score;
            }
        }

        public void ResetScore()
        {
            score = 0;
            ShowScore?.Invoke(score);
        }
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
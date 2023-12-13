using UnityEngine;
using Yudiz.ShootingGame.Base;

namespace Yudiz.ShootingGame.Manager
{
    public class ShootingGameManager : Singleton<ShootingGameManager>
    {
        #region PUBLIC_VARS
        public int totalQuestionCount;

        public int questionCount = 0;
        public QuestionDatabase questionDatabase;
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
        public void SetTotalQuestionCount()
        {
            totalQuestionCount = questionDatabase.questions.Length;
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
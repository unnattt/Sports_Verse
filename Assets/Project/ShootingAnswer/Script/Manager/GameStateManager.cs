using UnityEngine;

using Yudiz.ShootingGame.Base;
using Yudiz.ShootingGame.Utilities;

namespace Yudiz.ShootingGame.Manager
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        #region PUBLIC_VARS
        public GameStates currentGameState;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        public override void Awake()
        {
            base.Awake();
            currentGameState = GameStates.ChooseQuestionCategory;
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void ChangeGameState(GameStates state)
        {
            currentGameState = state;
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
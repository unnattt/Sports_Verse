using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRCricket.Core
{
    public static class GameEvents
    {
        #region Bowling
        public delegate void OnStanceSelect();
        public static OnStanceSelect rightStance;
        public static OnStanceSelect leftStance;

        public delegate void OnInvisibleBallSpawn();
        public static OnInvisibleBallSpawn StartInvisibleBallSpawn;

        public delegate void OnSpawningActuallBall(Vector3 forceVector, Vector3 bowlingHeight);
        public static OnSpawningActuallBall storeDataForActuallBall;

        public delegate void OnActualBallSpawn();
        public static OnActualBallSpawn StartActualBallSpawn;

        public delegate void OnSpawningMarker(Vector3 collsionPosition);
        public static OnSpawningMarker setMarker;
        #endregion

        #region UI
        public delegate void OnCountDown();
        public static OnCountDown countDown;

        public delegate void OnGameDataUpdate();
        public static OnGameDataUpdate updateGameData;

        public delegate void OnEachBallPlayed();
        public static OnEachBallPlayed updateDataAfterEachBall;

        public delegate void OnsettingDataForGamePlayScreen();
        public static OnsettingDataForGamePlayScreen setDataForGamePlayScreen;

        public delegate void OnSettingDataForPlayerTurnScreen();
        public static OnSettingDataForPlayerTurnScreen setDataForPlayerTurnScreen;

        public delegate void OnSettingDataForPlayerStatsScreen();
        public static OnSettingDataForPlayerStatsScreen setDataForPlayerStatsScreen;

        public delegate void OnWinning();
        public static OnWinning onWinning;

        public delegate void OnScoringHighRunRates(string playerName, float runRates);
        public static OnScoringHighRunRates calculateRunrates;
        #endregion

        #region VRHandsSwapControll
        public delegate void OnTurnCompleting();
        public static OnTurnCompleting enableHands;

        public delegate void OnTurnCompletion();
        public static OnTurnCompletion disableRightHand;
        public static OnTurnCompletion disableLeftHand;
        #endregion
    }
}
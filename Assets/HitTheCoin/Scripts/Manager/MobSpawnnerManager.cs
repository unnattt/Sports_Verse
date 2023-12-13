using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Yudiz.MiniGame.Spawner;
using Yudiz.MiniGame.Base;

namespace Yudiz.MiniGame.Manager
{
    public class MobSpawnnerManager : Singleton<MobSpawnnerManager>
    {
        #region PUBLIC_VARS
        public List<MobSpawner> mobSpawnList;
        public bool isMiniGameStarted;
        #endregion

        #region PRIVATE_VARS
        [SerializeField] int coinsMinSpawnTime;
        [SerializeField] int coinsMaxSpawnTime;
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS        
        #endregion

        #region PRIVATE_FUNCTIONS
        private int GetRandomCoinSpawnPoint()
        {
            int randomObjNo = Random.Range(0, mobSpawnList.Count);
            Debug.Log("Spawn No " + randomObjNo);
            return randomObjNo;
        }

        private int GetRandomSpawningNo()
        {
            int randomNo = Random.Range(coinsMinSpawnTime, coinsMaxSpawnTime);
            return randomNo;
        }
        #endregion

        #region CO-ROUTINES
        public IEnumerator StartMiniCoinGame()
        {
            isMiniGameStarted = true;
            while (isMiniGameStarted)
            {
                mobSpawnList[GetRandomCoinSpawnPoint()].GenerateRandomMob();
                yield return new WaitForSeconds(GetRandomSpawningNo());
            }
            yield return null;
        }
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
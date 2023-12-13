using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Yudiz.MiniGame.Manager;
using Yudiz.MiniGame.Prefab;

namespace Yudiz.MiniGame.Spawner
{
    public class MobSpawner : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] List<MobItem> mobList;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Transform coinMaxPoint;
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void GenerateRandomMob()
        {
            int randomNo = Random.Range(0, mobList.Count);
            Debug.Log("Random No " + randomNo);
            var obj = Instantiate(mobList[randomNo].gameObject, spawnPoint.position, mobList[randomNo].gameObject.transform.rotation, spawnPoint);
            obj.transform.localScale = new Vector3(3.333333f, 2, 8.333331f);            
            MoveObjUp(obj);
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        private void MoveObjUp(GameObject item)
        {
            item.transform.DOMoveY(coinMaxPoint.position.y, 1f).OnComplete(() => MoveObjDown(item));
            AudioManager.Instance.PlaySound(AudioName.CoinPop);
        }        

        private void MoveObjDown(GameObject obj)
        {
            obj.GetComponent<MobItem>().state = ItemState.Missed;
            Debug.Log("MovingObj Down");
            obj.transform.DOMoveY(spawnPoint.position.y, 1f).OnComplete(() => obj.GetComponent<MobItem>().Hide());            
        }        
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
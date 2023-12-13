using UnityEngine;

namespace Yudiz
{
    public class ObjInstantiateManager : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] InstantiateObjectItem instantiateObjectItem;
        [SerializeField] private AudioSource bgAudioPlayer;
        [SerializeField] float instantiateTime = 1;
        #endregion

        #region UNITY_CALLBACKS
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        [ContextMenu("Shoot")]
        public void ObjectSpawn()
        {
            InstantiateObjectItem item = Instantiate(instantiateObjectItem, this.transform);
            item.Shoot();
        }

        public void StartSpawing()
        {
            Debug.Log("StartSpawn");
            bgAudioPlayer.Play();
            InvokeRepeating("ObjectSpawn", 0, instantiateTime);
        }
        public void StopSpawning()
        {
            bgAudioPlayer.Stop();
            Debug.Log("StopSpawn");
            CancelInvoke("ObjectSpawn");
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
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using Yudiz.MiniGame.Manager;
using Yudiz.MiniGame.Prefab;

namespace Yudiz.MiniGame.Player
{
    public class PlayerItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] XRGrabInteractable xrHands;        
        [SerializeField] Vector3 inititalPosition;
        Vector3 localEulerAngles;
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            inititalPosition = transform.localPosition;
            localEulerAngles = transform.localEulerAngles;
            xrHands.selectEntered.AddListener(OnGrab);
            xrHands.selectExited.AddListener(OnUnGrab);
        }

        private void OnDestroy()
        {
            xrHands.selectEntered.AddListener(OnGrab);
            xrHands.selectExited.RemoveListener(OnUnGrab);
        }

        private void OnTriggerEnter(Collider other)
        {
            var collideObj = other.gameObject.GetComponent<MobItem>();
            if(collideObj != null && collideObj.state != ItemState.Missed)
            {
                collideObj.CoinCollect();
            }
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void OnGrab(SelectEnterEventArgs args0)
        {                       
            StartCoroutine(MobSpawnnerManager.Instance.StartMiniCoinGame());
            AudioManager.Instance.PlayBGMusic();
        }

        private void OnUnGrab(SelectExitEventArgs args0)
        {
            MobSpawnnerManager.Instance.isMiniGameStarted = false;            
            ResetStickPosition();
            AudioManager.Instance.StopBGMusic();
        }        

        private void ResetStickPosition()
        {
            transform.localPosition = inititalPosition;
            transform.localEulerAngles = localEulerAngles;
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
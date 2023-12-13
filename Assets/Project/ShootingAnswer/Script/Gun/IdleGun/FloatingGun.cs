using UnityEngine;

using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

namespace Yudiz.ShootingGame.Gun
{
    public class FloatingGun : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        Tween movingTween;
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            GunFreeFloating();
        }

        private void OnTriggerEnter(Collider other)        
        {
            Debug.Log("Collision Detected");
            var collideObj = other.gameObject.GetComponent<XRGunHandler>();
            if(collideObj != null)
            {
                KillAnimation();
                this.transform.gameObject.SetActive(false);
                collideObj.gunItem.SetActive(true);                
            }
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void GunFreeFloating()
        {
            movingTween = transform.DOMoveY(1.15f, 2.5f).SetLoops(-1, LoopType.Yoyo).SetAutoKill(false);                  
        }

        private void KillAnimation()
        {
            movingTween.SetLoops(0).SetAutoKill(true);
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
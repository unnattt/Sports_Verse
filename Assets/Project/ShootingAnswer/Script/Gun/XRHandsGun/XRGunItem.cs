using UnityEngine;
using UnityEngine.InputSystem;
using Yudiz.ShootingGame.Prefabs;
using static UnityEngine.InputSystem.InputAction;

namespace Yudiz.ShootingGame.Gun
{
    public class XRGunItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        [SerializeField] InputActionReference xrShootingTrigger;
        [SerializeField] BulletItem bulletItem;
        [SerializeField] Transform shootPoint;

        bool isTriggered;

        public float fireRate = 1.0f;
        private float lastFireTime = 0.0f;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void OnEnable()
        {
            xrShootingTrigger.action.performed += ShootBullets;
            xrShootingTrigger.action.canceled += StopShooting;
        }

        private void OnDisable()
        {
            xrShootingTrigger.action.performed -= ShootBullets;
            xrShootingTrigger.action.canceled -= StopShooting;
        }

        private void Update()
        {
            if (isTriggered && Time.time - lastFireTime >= fireRate)
            {
                BulletItem bullet = Instantiate(bulletItem, shootPoint.position, shootPoint.rotation);
                bullet.Fire(shootPoint.transform);

                lastFireTime = Time.time;
            }
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void ShootBullets(CallbackContext obj)
        {
            isTriggered = true;
        }

        private void StopShooting(CallbackContext obj)
        {
            isTriggered = false;
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
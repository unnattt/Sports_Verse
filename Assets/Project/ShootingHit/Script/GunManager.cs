using UnityEngine;

using Yudiz.ShootingGame.Prefabs;

namespace Yudiz.ShootingGame.Gun
{
    public class GunManager : MonoBehaviour
    {
        #region PUBLIC_VARS
        public Transform shootPoint;
        public BulletItem bulletItem;
        public float bulletFireTime = 1;

        public float sensitivity = 2.0f; // Adjust the sensitivity as needed
        public float fireRate = 1.0f;

        #endregion

        #region PRIVATE_VARS

        private float lastFireTime = 0.0f;
        private Vector2 rotation = Vector2.zero;
        #endregion

        #region UNITY_CALLBACKS

        private void Start()
        {
            //InvokeRepeating("BulletFire",1,bulletFireTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastFireTime >= fireRate) 
            {
                // Fire bullet
                BulletFire();
                lastFireTime = Time.time;
            }

            rotation.y += Input.GetAxis("Mouse X") * sensitivity;
            rotation.x += Input.GetAxis("Mouse Y") * sensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -90, 90); // Limit the vertical rotation

            transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotation.x, Vector3.left);
        }
    
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        [ContextMenu("Fire")]
        public void BulletFire()
        {
            BulletItem item = Instantiate(bulletItem,shootPoint.position, shootPoint.transform.rotation);
            item.Fire(shootPoint.transform);
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



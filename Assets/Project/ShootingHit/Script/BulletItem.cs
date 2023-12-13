using UnityEngine;

namespace Yudiz.ShootingGame.Prefabs
{
    public class BulletItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        public float zDirectionForce = 1;
        public Rigidbody rb;
        public float autoDestroyTime = 10;
        //public GameObject particalSysteam;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void Fire(Transform parent)
        {
            Debug.Log("Bullet Speed " + zDirectionForce);
            rb.AddForce(parent.forward* zDirectionForce, ForceMode.Impulse);
            Invoke("BulletAutoDestroy", autoDestroyTime);
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        public void BulletAutoDestroy()
        {
            Destroy(this.gameObject);
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
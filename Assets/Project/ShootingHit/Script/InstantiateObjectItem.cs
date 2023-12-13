using UnityEngine;
using System;
using UnityEditor;

namespace Yudiz
{
    public class InstantiateObjectItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] Rigidbody _rb;
        [SerializeField] int randomForceMinX = 0;
        [SerializeField] int randomForceMaxX = 0;
        [SerializeField] int randomForceMinY = 0;
        [SerializeField] int randomForceMaxY = 0;
        [SerializeField] int randomForceMinZ = 0;
        [SerializeField] int randomForceMaxZ = 0;
        [SerializeField] Vector2 angularForceRange;
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void Shoot()
        {
            _rb.AddForce(new Vector3(UnityEngine.Random.Range(randomForceMinX, randomForceMaxX), UnityEngine.Random.Range(randomForceMinY, randomForceMaxY), UnityEngine.Random.Range(randomForceMinZ, randomForceMaxZ)),ForceMode.Impulse);;
            _rb.angularVelocity = transform.up * UnityEngine.Random.Range(angularForceRange.x, angularForceRange.y);
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
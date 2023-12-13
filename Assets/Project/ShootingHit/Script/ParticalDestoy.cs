using UnityEngine;

namespace Yudiz
{
    public class ParticalDestoy : MonoBehaviour
    {
        #region PUBLIC_VARS
        public float destroidTime=1;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            Invoke("DestroidPartical", destroidTime);
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void DestroidPartical()
        {
            Destroy(this.gameObject);
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
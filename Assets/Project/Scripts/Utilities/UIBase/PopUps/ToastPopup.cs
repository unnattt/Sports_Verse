using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.BaseFramework;

namespace Yudiz.UI
{
    public class ToastPopup : UIScreenView
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] private TextMeshProUGUI toastDescriptionText;
        #endregion

        #region UNITY_CALLBACKS
        #endregion

        #region PUBLIC_METHODS
        public void SetData(string description, int delay = 3)
        {
            toastDescriptionText.text = description;
			this.Execute(() => Hide(), delay);
		}
        #endregion

    }
}

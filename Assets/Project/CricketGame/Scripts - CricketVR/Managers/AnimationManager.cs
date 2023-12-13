using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Yudiz.VRCricket.UI
{


    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void ScaleTransform(Transform scaleToTransform, float scaleFactor, float delayTime)
        {
            scaleToTransform.transform.DOScale(Vector3.one * scaleFactor, 0.2f).SetDelay(0.5f)
            .OnComplete(() =>
                {
                    scaleToTransform.transform.DOScale(Vector3.one, 0.2f);
                })
            .SetEase(Ease.OutBack)
            .SetDelay(delayTime);
        }
    }
}
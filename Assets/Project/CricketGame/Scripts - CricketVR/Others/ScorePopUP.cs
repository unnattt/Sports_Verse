using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Yudiz.VRCricket.Core
{
    public class ScorePopUP : MonoBehaviour
    {
        private void Start()
        {
            AnimatePopUP();
        }

        private void AnimatePopUP()
        {
            transform.DOScale(0.8f, 1);
        }
    }
}

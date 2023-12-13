using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRCricket.Core
{
    public class Bowler : MonoBehaviour
    {
        public void StartBolwingAnimation()
        {
            GameEvents.StartActualBallSpawn?.Invoke();
        }
    }
}
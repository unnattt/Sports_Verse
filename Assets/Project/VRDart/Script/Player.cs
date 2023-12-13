using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.PlayerPos
{
    public class Player : MonoBehaviour
    {
        

        public void OnTriggerStay(Collider other)
        {
            if (other.tag == "Boder")
            {
                ScreenManager._instance.alartScreen._alartScreenCanvas.enabled = true;
                Debug.Log("Go Back");
            }
        }


        public void OnTriggerExit(Collider other)
        {
            ScreenManager._instance.alartScreen._alartScreenCanvas.enabled = false;
        }
    }
}

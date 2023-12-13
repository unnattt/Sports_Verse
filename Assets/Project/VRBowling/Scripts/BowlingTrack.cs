using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yudiz.VRBowling
{
    public class BowlingTrack : MonoBehaviour
    {
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ball")
            {

                SoundManager.inst.SoundPlay(SoundManager.SoundName.BallIouch);
            }
        }
    }
}

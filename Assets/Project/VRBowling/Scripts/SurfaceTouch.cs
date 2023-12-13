using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Yudiz.VRBowling
{
    public class SurfaceTouch : MonoBehaviour
    {
        public async void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                SoundManager.inst.SoundPlay(SoundManager.SoundName.BallIouch);

                Debug.Log("----------SurfaceHit-------");
                StopAllCoroutines();
                GameManager.instance.TurnChange();
                await Task.Delay(100);
                Destroy(collision.gameObject);




            }
        }
    }
}

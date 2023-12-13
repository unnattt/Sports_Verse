using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.UI
{

    public class DisplayScreen : MonoBehaviour
    {
        //public Canvas _displayScreen;
        public GameObject dartboard;
        public GameObject LeaderboardLable;
        public GameObject animation;



        public async void AnimationStop()
        {
            await Task.Delay(1000);
            this.gameObject.SetActive(false);
            await Task.Delay(1000);
            //dartboard.SetActive(true);
            GameManager._instance.DartInstantiate();
        }
        public async void AnimationPlay()
        {
            Debug.Log("Play");
            LeaderboardLable.gameObject.SetActive(true);
            //dartboard.SetActive(false);
            await Task.Delay(3000);

        }


    }
}

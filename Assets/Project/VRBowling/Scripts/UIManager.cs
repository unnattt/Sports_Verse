using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRBowling
{
    public class UIManager : MonoBehaviour
    {
        public ScoreSytemCanvas _ScoreSytemCanvas;

        public MainMenuScreen menuScreen;
        public GameOverScreen _gameOverCanvas;


        public static UIManager instance;
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
        }


    }
}

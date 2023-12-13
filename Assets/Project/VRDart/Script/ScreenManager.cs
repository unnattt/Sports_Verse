using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yudiz.VRDart.UI;

namespace Yudiz.VRDart.Manager
{
    public class ScreenManager : MonoBehaviour
    {

        public GameObject animationScreen;
        public TMP_Text playerName;
        public MenuScreen menuScreen;
        public GameSelectionScreen gameSelectionScreen;
        public GameOverScreen gameOverScreen;
        public DisplayScreen displayScreen;
        public AlartScreen alartScreen;
        public GameObject gameOver;


        public static ScreenManager _instance;

        public void Awake()
        {
            _instance = this;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Yudiz.VRCricket.UI
{
    public class GameInfoTextCanvas : BaseScreen
    {
        [SerializeField] private  TextMeshProUGUI infoText;

        private const string message = "Ready To Play? The game begins when you grab the bat !";

        private void Start()
        {
            infoText.text = message;
        }
    }
}
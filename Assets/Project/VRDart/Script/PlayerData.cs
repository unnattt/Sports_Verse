using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Yudiz.VRDart.Data
{
    public class PlayerData : MonoBehaviour
    {
        public TMP_Text playerName;
        public TMP_Text playerScore;
        public int score;

        public void SetScore(int value)
        {
            score = value;
            playerScore.text = value.ToString();
        }
    }
}

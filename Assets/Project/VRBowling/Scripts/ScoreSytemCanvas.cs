using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Yudiz.VRBowling
{
    public class ScoreSytemCanvas : MonoBehaviour
    {
        public TMP_Text R1ChanceOne;
        public TMP_Text R1ChanceTwo;

        public TMP_Text R2ChanceOne;
        public TMP_Text R2ChanceTwo;

        public TMP_Text R3ChanceOne;
        public TMP_Text R3ChanceTwo;

        public TMP_Text R4ChanceOne;
        public TMP_Text R4ChanceTwo;

        public TMP_Text R5ChanceOne;
        public TMP_Text R5ChanceTwo;

        public TMP_Text FinalScore;


        public List<Image> roundImage;

        public int HighScore;
        public int CurrentScore;


    }
}

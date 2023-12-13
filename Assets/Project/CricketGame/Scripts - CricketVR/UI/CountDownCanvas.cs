using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{
    public class CountDownCanvas : BaseScreen
    {
        [SerializeField] private int countDownTime;
        [SerializeField] Text countDowntext;

        private int resetTime = 3;
        private float countDowndelay = 1f;
        private const string emptyString = "";

        private void OnEnable()
        {
            GameEvents.countDown += StartTimer;
        }

        private void OnDisable()
        {
            GameEvents.countDown -= StartTimer;
        }

        public void StartTimer()
        {
            StartCoroutine(nameof(CountDownTostart));
        }

        IEnumerator CountDownTostart()
        {
            GameEvents.StartInvisibleBallSpawn?.Invoke();

            while (countDownTime > 0 )
            {
                countDowntext.text = countDownTime.ToString();

                yield return new WaitForSeconds(countDowndelay);

                countDownTime--;
            }
            countDowntext.text = "GO !";

            StartTheGame();

            yield return new WaitForSeconds(countDowndelay);

            countDowntext.text = emptyString;
            countDownTime = resetTime;            
        }

        public void StartTheGame()
        {
            GameManager.inst.StartBowlingAnimation();
        }
    }
}
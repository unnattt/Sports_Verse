using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.VRCricket.UI;

namespace Yudiz.VRCricket.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BatGrabInteractable bat;
        [SerializeField] private Animator bowlerAnimator;

        [Header("Controller Hands")]
        [SerializeField] private AnimateHandOnInput leftHands;
        [SerializeField] private AnimateHandOnInput rightHands;

        [Header("CountDownDelay")]
        [SerializeField] float countdownDelay;

        public int playerIndex = 0;
        private int wicketsPerPlayer = 2;

        private float actualTimeStemp;
        private float gameTimeStemp = 0.01f;

        private BatGrabInteractable batInstance;

        public static GameManager inst;
        private void Awake()
        {
            inst = this;
        }

        private void OnEnable()
        {
            GameEvents.disableLeftHand += DisableleftHand;
            GameEvents.disableRightHand += DisableRightHand;
            GameEvents.enableHands += EnableHands;

            actualTimeStemp = Time.fixedDeltaTime;
            Debug.Log("Fixed Timestemp" + Time.fixedDeltaTime);

            Time.fixedDeltaTime = gameTimeStemp;
            Debug.Log("Fixed Timestemp" + Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            GameEvents.disableLeftHand -= DisableleftHand;
            GameEvents.disableRightHand -= DisableRightHand;
            GameEvents.enableHands -= EnableHands;

            Time.fixedDeltaTime = actualTimeStemp;
            Debug.Log("Fixed Timestemp" + Time.fixedDeltaTime);
        }

        private void Start()
        {
            SpawnAndDisableBat();
        }

        public void StartGame()
        {
            StartBowling();
        }

        public void StartBowlingAnimation()
        {
            bowlerAnimator.SetTrigger("StartBowling");
        }

        public void StartBowling()
        {
            StartCoroutine(nameof(StartCountDown));
        }

        public IEnumerator StartCountDown()
        {
            yield return new WaitForSeconds(countdownDelay);

            ScreenManager.instance.SwitchScreen(ScreenName.countDownScreen);
            GameEvents.countDown?.Invoke();
            AudioManager.inst.PlayAudio(AudioManager.AudioName.CountDownSound);
        }

        public void SpawnAndDisableBat()
        {
            Debug.Log("Bat Spawned");

            batInstance = Instantiate(bat, transform.position, Quaternion.identity);
            batInstance.gameObject.SetActive(false);
        }

        public void EnableBat()
        {
            Debug.Log("Bat Enabled");

            batInstance.gameObject.SetActive(true);
        }

        public void CurrentPlayersTurnOver()
        {
            Debug.Log("Current Players Turn Over");

            OversManager.instance.CalculateRunrates();

            GameEvents.enableHands?.Invoke();

            if (batInstance != null)
            {
                Destroy(batInstance.gameObject);
                Debug.Log("BatDestroyed");
            }
            else
            {
                Debug.Log("Bat ==== Null");
            }
            SpawnAndDisableBat();

            ShowPlayerStats();
        }

        public void ShowPlayerStats()
        {
            ScoreManager.inst.SetDataForPlayerStatsScreen();
            ScreenManager.instance.SwitchScreen(ScreenName.PlayerStatsScreen);
        }

        public void StartNextPlayerTurn()
        {
            Debug.Log("Next Player Turn");

            playerIndex++;
            ScoreManager.inst.UpdateCurrentPLayerIndex(playerIndex);
            ScoreManager.inst.UpdatePlayerName();
            ScoreManager.inst.SetDataForPlayerTurnScreen();
            ScreenManager.instance.SwitchScreen(ScreenName.PlayerTurnScreen);
        }

        public void SetDataForNextPlayer()
        {
            OversManager.instance.ResetData();
            OversManager.instance.wicketsPerPlayer = wicketsPerPlayer;

            ScoreManager.inst.ResetScores();
            ScoreManager.inst.UpdateCurrentWickets(wicketsPerPlayer);
        }

        private void EnableHands()
        {
            leftHands.gameObject.SetActive(true);
            rightHands.gameObject.SetActive(true);
        }

        private void DisableRightHand()
        {
            rightHands.gameObject.SetActive(false);
        }

        private void DisableleftHand()
        {
            leftHands.gameObject.SetActive(false);
        }
    }
}

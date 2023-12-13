using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Yudiz.VRCricket.Core
{
    public class OversManager : MonoBehaviour
    {
        public static OversManager instance;

        public float runrate;
        public int wicketsLost = 0;
        public int wicketsPerPlayer = 2;

        [Header("Overs Data")]
        [SerializeField] private int overPlayed = 0;
        [SerializeField] public int oversPerPlayer;
        [SerializeField] private int totalBallsPerPlayer;

        public int ballPlayed;
        private int ballsPlayed;
        private int ballsPerOver = 6;

        [Header("Stumps Position")]
        [SerializeField] Transform[] stumps;
        [SerializeField] GameObject[] stumpsobj;
        private Vector3[] stumpOriginalPositions;
        private Quaternion[] stumpOriginalRotations;


        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            GameEvents.updateDataAfterEachBall += ResetStumpsPosition;
            GameEvents.updateDataAfterEachBall += BallDecrement;
            GameEvents.updateDataAfterEachBall += UpdatePlayerWickets;

            GameEvents.setDataForGamePlayScreen += UpdatePlayerWickets;
            GameEvents.setDataForGamePlayScreen += UpdateBalls;
            GameEvents.setDataForGamePlayScreen += CalculateOvers;
            GameEvents.setDataForGamePlayScreen += CalculateBallsOvers;
        }

        private void OnDisable()
        {
            GameEvents.updateDataAfterEachBall -= ResetStumpsPosition;
            GameEvents.updateDataAfterEachBall -= BallDecrement;
            GameEvents.updateDataAfterEachBall -= UpdatePlayerWickets;

            GameEvents.setDataForGamePlayScreen -= UpdatePlayerWickets;
            GameEvents.setDataForGamePlayScreen -= UpdateBalls;
            GameEvents.setDataForGamePlayScreen -= CalculateOvers;
            GameEvents.setDataForGamePlayScreen -= CalculateBallsOvers;
        }

        private void Start()
        {
            StoreStumpsPosition();
        }

        private void StoreStumpsPosition()
        {
            stumpOriginalPositions = new Vector3[stumps.Length];
            stumpOriginalRotations = new Quaternion[stumps.Length];

            for (int i = 0; i < stumps.Length; i++)
            {
                stumpOriginalPositions[i] = stumps[i].position;
                stumpOriginalRotations[i] = stumps[i].rotation;
            }
        }

        private void ResetStumpsPosition()
        {
            for (int i = 0; i < stumps.Length; i++)
            {
                stumps[i].position = stumpOriginalPositions[i];
                stumps[i].rotation = stumpOriginalRotations[i];
            }

            for (int i = 0; i < stumpsobj.Length; i++)
            {
                Rigidbody rb = stumpsobj[i].GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        public void BallDecrement()
        {
            Debug.Log("Ball Decrement");

            ballPlayed++;
            ballsPlayed++;
            totalBallsPerPlayer--;

            ScoreManager.inst.UpdateBalls(totalBallsPerPlayer);
            ScoreManager.inst.SetDataForGamePlayScreen();

            if (totalBallsPerPlayer <= 0 || wicketsPerPlayer <= 0 || ScoreManager.inst.players[1].totalRuns > ScoreManager.inst.players[0].totalRuns)
            {
                Debug.Log("TurnOver");
                GameManager.inst.CurrentPlayersTurnOver();
            }
            else
            {
                GameManager.inst.StartBowling();
                Debug.Log("Started Bowling In Else");
            }
        }

        public void SetPlayerWickets(int wicketsToSet)
        {
            wicketsPerPlayer -= wicketsToSet;
            wicketsLost += wicketsToSet;

            ScoreManager.inst.UpdateCurrentWickets(wicketsPerPlayer);
        }

        public void UpdatePlayerWickets()
        {
            ScoreManager.inst.UpdateCurrentWickets(wicketsPerPlayer);
        }

        public void SetOversForEachPlayer(int oversToSet)
        {
            Debug.Log("Overs Set");

            oversPerPlayer = oversToSet;
            totalBallsPerPlayer = oversPerPlayer * ballsPerOver;

            ScoreManager.inst.UpdateOvers(oversToSet);
        }

        public void UpdateBalls()
        {
            ScoreManager.inst.UpdateBalls(totalBallsPerPlayer);
        }

        public void ResetOvers()
        {
            Debug.Log("Over Resetted");

            totalBallsPerPlayer = oversPerPlayer * ballsPerOver;
            ScoreManager.inst.UpdateBalls(totalBallsPerPlayer);
        }

        public void CalculateOvers()
        {
            if (ballsPlayed >= ballsPerOver)
            {
                ScoreManager.inst.currentOverLeft--;
                overPlayed++;
                ballsPlayed = 0;
            }
        }

        public void CalculateRunrates()
        {
            int currentPlayerScore = ScoreManager.inst.currentPlayerScore;
            float oversPlayed = ballPlayed / 6.0f;

            if (oversPlayed > 0)
            {
                runrate = (float)currentPlayerScore / oversPlayed;
            }
            else
            {
                runrate = (float)currentPlayerScore;
            }

            Debug.Log("RunRate: " + runrate);

            GameEvents.calculateRunrates?.Invoke(ScoreManager.inst.currentplayerName, runrate);
        }

        private void CalculateBallsOvers()
        {
            float round = Mathf.Round(ballPlayed / ballsPerOver);
            float mod = ballPlayed % ballsPerOver;

            ScoreManager.inst.oversBall = $"{round}.{mod}";
        }

        public void ResetData()
        {
            ResetOvers();
            overPlayed = 0;
            wicketsLost = 0;
            ballPlayed = 0;
        }
    }
}

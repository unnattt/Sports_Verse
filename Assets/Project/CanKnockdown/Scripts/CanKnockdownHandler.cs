using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CanKnockdown
{
    public class CanKnockdownHandler : MonoBehaviour
    {
        [HideInInspector] public List<Ball> balls;
        public Ball ballPrefab;
        public Transform initialPointBall;
        public Transform initialPointLeval;
        public float spawnOffset;

        public int ballCount;
        public GameObject parent;
        public GameObject Levalprefab;
        [HideInInspector] public GameObject CurrentLeval;

        private int ballThrownCount;

        void Start()
        {
            BallInstanciat();
            LevalInstanciat();
        }

        public void BallInstanciat()
        {
            for (int i = 0; i < ballCount; i++)
            {
                Ball obj = Instantiate(ballPrefab, new Vector3(initialPointBall.position.x + (i * spawnOffset), initialPointBall.position.y, initialPointBall.position.z), initialPointBall.rotation, initialPointBall.transform);
                obj.onBallReleased.AddListener(OnBallRelease);
                balls.Add(obj);
            }
            ballThrownCount = ballCount;
        }

        private void OnBallRelease(Ball arg0)
        {
            Throwball();
            arg0.onBallReleased.RemoveListener(OnBallRelease);
        }

        public void LevalInstanciat()
        {
            GameObject obj = Instantiate(Levalprefab, initialPointLeval.position, initialPointLeval.rotation, initialPointLeval.transform);
            CurrentLeval = obj;
        }


        public async void Throwball()
        {
            ballThrownCount--;
            if (ballThrownCount <= 0)
            {
                await Task.Delay(3000);
                foreach(var ball in balls)
                {
                    Destroy(ball.gameObject);
                }
                balls.Clear();
                BallInstanciat();
                Destroy(CurrentLeval);
                LevalInstanciat();
            }
        }
    }
}
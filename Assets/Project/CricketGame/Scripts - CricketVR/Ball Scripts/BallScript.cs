using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit;


namespace Yudiz.VRCricket.Core
{
    public class BallScript : MonoBehaviour
    {
        [Header("Ball CurrentState")]
        [SerializeField] private BallState ballState = BallState.InAir;

        [Space(10)]
        [Header("Bounce Particle Ref")]
        [SerializeField] private GameObject bounceParticlePrefab;

        [Space(10)]
        [Header("Force After Hitting Bat")]
        [SerializeField] private float forceAmount;

        private Rigidbody ballRigidbody;

        private float popUpOffSet = 5;
        private float yoffsetValue = 10;
        private float spawnInterval = 1.0f;
        private float velocityThreshold = 0.4f;
        private float timeSinceLastSpawn = 0.0f;
        private float materialBounciness = 0.7f;

        //Global Variables
        private Vector3 yOffset;
        private Vector3 batCollisionPoint;

        private void Awake()
        {
            ballRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SetRequiredPositions();
        }

        private void FixedUpdate()
        {
            MaintainTiming();
        }

        public void Shoot(Vector3 forceVector)
        {
            ballRigidbody.AddForce(forceVector);
        }


        private void MaintainTiming()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                VelocityCheck();
                timeSinceLastSpawn = 0.0f;
            }
        }

        public void SetRequiredPositions()
        {
            yOffset = Vector3.up * yoffsetValue;
        }

        public void VelocityCheck()
        {
            if (ballRigidbody.velocity.magnitude < velocityThreshold)
            {
                Debug.Log("Magnitude < velocityThreshold");

                HandleNormalScores();
                GameEvents.updateDataAfterEachBall?.Invoke();
                DestroyBall();
            }
        }

        public void ApplyExtraVelocity(float velocityIncrease)
        {
            Debug.Log("Extra Force Applied");
            Vector3 currentVelocity = ballRigidbody.velocity;
            Vector3 newVelocity = currentVelocity * velocityIncrease;
            ballRigidbody.velocity = newVelocity;
        }


        #region HandleCollisions
        private void HandleBatCollision(GameObject bat)
        {
            BatGrabInteractable batGrabInteractable = bat.GetComponent<BatGrabInteractable>();

            if (batGrabInteractable != null)
            {
                batGrabInteractable.Triggerhaptics();

                AudioManager.inst.PlayAudio(AudioManager.AudioName.BallHit);

                ApplyExtraVelocity(forceAmount);

                SphereCollider sphereCollider = GetComponent<SphereCollider>();
                if (sphereCollider != null)
                {
                    Debug.Log("Material Found");
                    sphereCollider.material.bounciness = materialBounciness;
                }
                else
                {
                    Debug.Log("Material Not Found");
                }

                batCollisionPoint = transform.position;
            }
        }

        private void HandleGroundCollision(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                ContactPoint contact = collision.contacts[0];

                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
                Vector3 pos = contact.point;

                GameObject bounceEffect = Instantiate(bounceParticlePrefab, pos, rot);
                Destroy(bounceEffect, 0.6f);
            }
        }

        private void HandleStumpsCollision()
        {
            Debug.Log("Bowled");
            ScoreManager.inst.HandlePopups("Bowled !", ScoreManager.inst.transform.position, batCollisionPoint);
            OversManager.instance.SetPlayerWickets(1);
            AudioManager.inst.PlayAudio(AudioManager.AudioName.stumped);
        }
        #endregion


        #region HandleTriggers
        private void HandleMissTrigger(GameObject wicketKeeper)
        {
            if (wicketKeeper.CompareTag("WicketKeeper") && ballState == BallState.InAir)
            {
                Debug.Log("Ball Missed");
                ScoreManager.inst.HandlePopups("Missed !", ScoreManager.inst.transform.position, batCollisionPoint);
                GameEvents.updateDataAfterEachBall?.Invoke();
                DestroyBall();
            }
        }

        private void HandleBoundaryTrigger(GameObject boundary)
        {
            if (boundary.CompareTag("Boundary"))
            {
                switch (ballState)
                {
                    case BallState.HitGround:
                        HandleFours();
                        break;

                    case BallState.HitBat:
                        HandleSixes();
                        break;
                }

                Debug.Log("Boundary !!");

                GameEvents.updateDataAfterEachBall?.Invoke();
                DestroyBall();
            }
        }
        #endregion


        #region HandleScores
        private void HandleFours()
        {
            Debug.Log("Four !!");
            ScoreManager.inst.AddScore(4);
            ScoreManager.inst.UpdateTotalFours(1);
            ScoreManager.inst.HandlePopups("+4", transform.position + yOffset, batCollisionPoint);
            ScoreManager.inst.UpdateTotalBoundaries(1);
        }

        private void HandleSixes()
        {
            Debug.Log("Six !!");
            ScoreManager.inst.AddScore(6);
            ScoreManager.inst.UpdateTotalSixes(1);
            ScoreManager.inst.HandlePopups("+6", transform.position + yOffset, batCollisionPoint);
            ScoreManager.inst.UpdateTotalBoundaries(1);
        }

        private void HandleNormalScores()
        {
            if (ballRigidbody.velocity.magnitude < velocityThreshold && ballState == BallState.HitGround)
            {
                float travelDistance = Vector3.Distance(transform.position, batCollisionPoint);
                Vector3 popUpPos = new Vector3(transform.position.x, transform.position.y + popUpOffSet, transform.position.z);

                ScoreManager.inst.HandleNormalScores(travelDistance, popUpPos, batCollisionPoint);

                ballState = BallState.Stopped;
            }
        }
        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            HandleGroundCollision(collision);

            switch (ballState)
            {
                case BallState.InAir when collision.gameObject.CompareTag("Bat"):
                    ballState = BallState.HitBat;
                    HandleBatCollision(collision.gameObject);
                    break;

                case BallState.HitBat when collision.gameObject.CompareTag("Ground"):
                    ballState = BallState.HitGround;
                    //HandleGroundCollision(collision);
                    break;

                case BallState.InAir when collision.gameObject.CompareTag("Stumps"):
                    ballState = BallState.HitStumps;
                    HandleStumpsCollision();
                    break;

                case BallState.HitBat when collision.gameObject.CompareTag("Stumps"):
                    ballState = BallState.HitStumps;
                    HandleStumpsCollision();
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleMissTrigger(other.gameObject);

            HandleBoundaryTrigger(other.gameObject);
        }

        private void DestroyBall()
        {
            Destroy(gameObject);
        }
    }
}


public enum BallState
{
    InAir,
    HitBat,
    HitGround,
    HitBoundary,
    HitStumps,
    Stopped
}


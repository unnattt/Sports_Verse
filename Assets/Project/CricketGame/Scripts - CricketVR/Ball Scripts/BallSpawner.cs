using UnityEngine;
using Unity.Collections;
using System.Collections;

namespace Yudiz.VRCricket.Core
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private Bowler bowler;

        [Header("Marker delay")]
        [SerializeField] private float enablemarkerDelay = 3f;
        [SerializeField] private float disablemarkerDelay = 3f;

        [Space(10)]
        [Header("Prefabs Refrences")]
        [SerializeField] private BallScript _ballPrefab;
        [SerializeField] private InvisibleBallScript invisibleBall;
        [SerializeField] private GameObject marker;

        //GLobal Variables
        private GameObject markerInstance;
        private Vector3 bowlingHeight;
        private BallScript ballInstance;

        //ball X random Targets
        private float rightStanceMinX = -0.4f;
        private float rightStanceMaxX = -0.7f;
        private float leftStanceMinX = 0.4f;
        private float leftStanceMaxX = 0.7f;

        [Space(10)]
        [Header("Bowling Sides Position")]
        [SerializeField] private Transform leftHandSide;
        [SerializeField] private Transform rightHandSide;

        [SerializeField] private Transform leftSide;
        [SerializeField] private Transform rightSide;

        private Vector3 actualballforcevector;
        private Vector3 actualballSpawnHeight;

        [Space(10)]
        [Header("Bowler Height Point")]
        [SerializeField] private float BowlerMinY;
        [SerializeField] private float BowlerMaxY;

        [Space(10)]
        [Header("Throw Target Position")]
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField] private float minZ;
        [SerializeField] private float maxZ;


        private void OnEnable()
        {
            GameEvents.rightStance += RightSideBowling;
            GameEvents.leftStance += LeftSideBowling;

            GameEvents.StartInvisibleBallSpawn += StartBowling;
            GameEvents.StartActualBallSpawn += SpawnActualBall;
            GameEvents.setMarker += SetMarker;

            GameEvents.storeDataForActuallBall += ActuallBallData;
        }

        private void OnDisable()
        {
            GameEvents.rightStance -= RightSideBowling;
            GameEvents.leftStance -= LeftSideBowling;

            GameEvents.StartInvisibleBallSpawn -= StartBowling;
            GameEvents.StartActualBallSpawn -= SpawnActualBall;
            GameEvents.setMarker -= SetMarker;

            GameEvents.storeDataForActuallBall -= ActuallBallData;
        }

        private void Start()
        {
            SpawnMarker();
        }

        public void StartBowling()
        {
            SpawnInvisibleBall();
        }

        private void SpawnInvisibleBall()
        {
            Debug.Log("Invisible BallSpawned");

            float randomHeight = Random.Range(BowlerMinY, BowlerMaxY);
            bowlingHeight = new Vector3(transform.position.x, randomHeight, transform.position.z);

            InvisibleBallScript invisibleballInstance = Instantiate(invisibleBall, bowlingHeight, Quaternion.identity);
            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

            invisibleballInstance.ShootInvisibleBall(randomPos, bowlingHeight);
        }

        private void ActuallBallData(Vector3 forcevector, Vector3 bowlersheight)
        {
            actualballforcevector = forcevector;
            actualballSpawnHeight = bowlersheight;
        }

        private void SpawnActualBall()
        {
            ballInstance = Instantiate(_ballPrefab, actualballSpawnHeight, Quaternion.identity);
            ballInstance.Shoot(actualballforcevector);
        }

        private void SpawnMarker()
        {
            Debug.Log("Marker Spawned and Disabled");

            markerInstance = Instantiate(marker, transform.position, Quaternion.identity);
            DisableMarker();
        }
        private void SetMarker(Vector3 position)
        {
            Debug.Log("Marker Set");

            markerInstance.transform.position = position;
            Invoke(nameof(EnableMarker), enablemarkerDelay);
        }

        private void EnableMarker()
        {
            Debug.Log("Marker Enable");

            markerInstance.SetActive(true);
            Invoke(nameof(DisableMarker), disablemarkerDelay);
        }

        private void DisableMarker()
        {
            Debug.Log("Marker Disable");

            markerInstance.SetActive(false);
        }

        public void RightSideBowling()
        {
            transform.position = rightHandSide.position;
            bowler.transform.position = rightSide.position;

            minX = rightStanceMinX;
            maxX = rightStanceMaxX;
        }

        public void LeftSideBowling()
        {
            transform.position = leftHandSide.position;
            bowler.transform.position = leftSide.position;

            minX = leftStanceMinX;
            maxX = leftStanceMaxX;
        }
    }
}
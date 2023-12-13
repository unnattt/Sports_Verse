using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yudiz.VRCricket.Core
{ 
    public class InvisibleBallScript : MonoBehaviour
    {
        [Header("Ball Speed")]
        [SerializeField] private float MindesiredBallSpeed;
        [SerializeField] private float MaxdesiredBallSpeed;

        [Header("Marker")]
        [SerializeField] private float markeroffset;


        public void ShootInvisibleBall(Vector3 targetPos, Vector3 bowlingHeight)
        {
            Rigidbody invisbleBallRigidBody = GetComponent<Rigidbody>();

            float randomSpeed = Random.Range(MindesiredBallSpeed, MaxdesiredBallSpeed);
            Vector3 forceVector = (targetPos - transform.position).normalized * randomSpeed;

            invisbleBallRigidBody.AddForce(forceVector);

            GameEvents.storeDataForActuallBall?.Invoke(forceVector, bowlingHeight);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Vector3 collisionPoint = new Vector3(transform.position.x, transform.position.y - markeroffset, transform.position.z);

                GameEvents.setMarker?.Invoke(collisionPoint);

                Destroy(gameObject);
            }
        }
    }
}

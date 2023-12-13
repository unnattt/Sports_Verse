using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Yudiz.VRArchery.Managers;

namespace Yudiz.VRArchery.CoreGameplay
{
    public class Bow : MonoBehaviour
    {
        #region PUBLIC_VARS
        [Header("Bow Area")]
        public Transform arrowStartPoint;
        public Transform arrowEndPoint;
        public Transform pointBetweenStartAndEnd;
        public LineRenderer trajectoryLine;
        public bool isGrabingBow;
        #endregion

        #region PRIVATE_VARS        
        private Vector3 initialArrowPos;
        private Vector3 initialPos;
        private Quaternion initialRotation;
        [SerializeField] private LineRenderer bowString;
        //[SerializeField] private float speed;
        [SerializeField] private XRGrabInteractable xrGrabInteractable;
        [SerializeField] private Rigidbody bowRigidbody;
        [SerializeField] private float gravity = 9.8f;
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            isGrabingBow = false;
            trajectoryLine.enabled = false;
            initialArrowPos = pointBetweenStartAndEnd.localPosition;
            initialPos = transform.position;
            initialRotation = transform.rotation;
            xrGrabInteractable.selectEntered.AddListener(OnGrabingBow);
            xrGrabInteractable.selectExited.AddListener(OnLeavingBOwCall);
        }
        #endregion

        #region PUBLIC_FUNCTIONS
        public void BowThrower(float forcePower, Arrow arrow)
        {
            arrow.transform.position = arrow.ModelArrow.transform.position;
            arrow.ModelArrow.localPosition = Vector3.zero;
            Vector3 force = forcePower * arrow.transform.forward;
            arrow.Thrower(force);
            trajectoryLine.enabled = false;
            //UnAssignArrow();
        }

        public void ResetStringPos()
        {
            pointBetweenStartAndEnd.localPosition = initialArrowPos;
            UpdatePullingString(pointBetweenStartAndEnd.localPosition);
        }

        public void UpdatePullingString(Vector3 updatedString)
        {
            Vector3 linePosition = updatedString;
            bowString.SetPosition(1, linePosition);
            //bowString2.SetPosition(1, linePosition);
        }

        public void DrawProjection(float intialForce, LineRenderer line, Rigidbody rb)
        {
            float resolutionValue = GameController.inst.PullValueForResolution(intialForce);
            int resolution = (int)resolutionValue;
            Debug.Log("Resolution: " + resolution);
            float TimeBetweenPoints = 0.1f;
            Transform ReleasePosition = transform;

            //line.positionCount = Mathf.CeilToInt(resolution / TimeBetweenPoints) + 1;
            Vector3 startPosition = ReleasePosition.position;
            Vector3 startVelocity = intialForce * transform.forward / rb.mass;
            int i = 0;
            line.SetPosition(i, startPosition);

            List<Vector3> points = new List<Vector3>();
            Vector3 previousPosition = transform.position;


            for (float time = 0; time < resolution; time += TimeBetweenPoints)
            {
                i++;
                Vector3 point = startPosition + time * startVelocity;
                point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
                float dis = Vector3.Distance(previousPosition, point);

                points.Add(point);
                //Debug.Log("Distance Points: " + dis);
                if (dis > 1f)
                {
                    line.positionCount = i;
                    line.SetPositions(points.ToArray());
                    //Debug.Log("Break");
                    //line.SetPosition(i, point);
                    break;
                }
            }

        }
        //Vector3 point;

        //Material lineMat = line.material;
        //RaycastHit hit;
        //if (Physics.Raycast(previousPosition, point - previousPosition, out hit, Vector3.Distance(previousPosition, point)))
        //{
        //    if (hit.collider.GetComponent<Enemy>() != null)
        //    {
        //        lineMat.SetColor("_BaseColor", Color.green);
        //    }
        //    else
        //    {
        //        lineMat.SetColor("_BaseColor", Color.white);
        //    }
        //    hasHit = true;
        //    points.Add(point);
        //}
        //else
        //    points.Add(point);
        //// Update the previous position
        //previousPosition = point;        

        public void CalculateHalfTrajectory(float initialForce, LineRenderer line, Rigidbody rb)
        {
            Vector3 initialPosition = transform.position;
            // Calculate the initial velocity vector
            Vector3 initialVelocityVector = transform.forward * (initialForce / rb.mass);

            // Calculate the time of flight
            float timeOfFlight = (
                initialVelocityVector.magnitude * Mathf.Sin(transform.forward.y)) / gravity;

            //float timeOfFlight = 0.05f;            
            // Calculate the trajectory at each time step
            int resolution = 100;
            float timeStep = timeOfFlight / resolution;

            List<Vector3> points = new List<Vector3>();

            // Calculate and add points until the projectile reaches its highest point
            for (float t = 0; t <= timeOfFlight; t += timeStep)
            {
                float x = initialPosition.x + (initialVelocityVector.x * t);
                float y = initialPosition.y + (initialVelocityVector.y * t) - (0.5f * gravity * t * t);
                float z = initialPosition.z + (initialVelocityVector.z * t);

                Vector3 reachDistance = new Vector3(x, y, z);
                float dis = Vector3.Distance(initialPos, reachDistance);
                if (dis > 4)
                {
                    break;
                }

                points.Add(reachDistance);
            }

            line.positionCount = points.Count;
            //line.positionCount = (int)(timeOfFlight / timeStep);
            line.SetPositions(points.ToArray());
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        private void OnLeavingBOwCall(SelectExitEventArgs arg0)
        {
            bowRigidbody.isKinematic = false;
            //arg0.interactorObject.transform.GetChild(0).gameObject.SetActive(true);
            Invoke(nameof(ResetBowPos), 0.3f);
        }

        private void OnGrabingBow(SelectEnterEventArgs arg0)
        {
            bowRigidbody.isKinematic = true;
            isGrabingBow = true;
            //if (arg0.interactorObject is XRDirectInteractor)
            //{
            //    arg0.interactorObject.transform.GetChild(0).gameObject.SetActive(false);
            //}
        }

        void ResetBowPos()
        {
            bowRigidbody.isKinematic = true;
            isGrabingBow = false;
            transform.SetPositionAndRotation(initialPos, initialRotation);
        }
        #endregion

        #region CO-ROUTINES
        #endregion
        #region EVENT_HANDLERS
        #endregion
        #region UI_CALLBACKS
        #endregion
    }
}

#region COMMENTS_REGION
//private void CalculateTrajectory(float velocity)
//public float Remap(float unscaledNum, float minAllowed, float maxAllowed, float min, float max)
//{
//    return (maxAllowed - minAllowed) * (unscaledNum - min) / (max - min) + minAllowed;
//}
//public void CalculateTrajectory(float initialForce, LineRenderer line, Rigidbody rb)
//{
//    Vector3 initialPosition = transform.position;
//    // Calculate the initial velocity vector
//    //Vector3 initialVelocityVector = transform.forward * (initialForce / rb.mass);
//    Vector3 initialVelocityVector = initialForce * (transform.forward / rb.mass);


//    // Calculate the time of flight
//    float timeOfFlight = (2f * initialVelocityVector.magnitude * Mathf.Sin(transform.forward.y)) / gravity;
//    //float halfTimeOfFlight = timeOfFlight / 2f; // Calculate half of the time of flight

//    // Calculate the trajectory at each time step
//    int resolution = 100;
//    //float timeStep = halfTimeOfFlight / resolution;
//    //float timeStep;

//    float timeStep = timeOfFlight / resolution;
//    //timeStep = timeOfFlight / initialForce;


//    List<Vector3> points = new List<Vector3>();
//    for (float t = 0; t <= timeOfFlight; t += timeStep)
//    {
//        float x = initialPosition.x + initialVelocityVector.x * t;
//        float y = initialPosition.y + initialVelocityVector.y * t - (0.5f * gravity * t * t);
//        float z = initialPosition.z + initialVelocityVector.z * t;

//        Vector3 reachDistance = new(x, y, z);
//        float dis = Vector3.Distance(initialPos, reachDistance);
//        if (dis > 3)
//        {
//            break;
//        }
//        points.Add(reachDistance);
//    }

//    //line.positionCount = points.Count;
//    line.positionCount = (int)(timeOfFlight / timeStep) / 2;
//    line.SetPositions(points.ToArray());
//}

//private List<Vector3> SimulateArc(float _force, Rigidbody rb)
//{
//    List<Vector3> lineRendererPoints = new List<Vector3>(); //Reset LineRenderer List for new calculation

//    float maxDuration = 5f; //INPUT amount of total time for simulation
//    float timeStepInterval = 0.1f; //INPUT amount of time between each position check
//    int maxSteps = (int)(maxDuration / timeStepInterval);//Calculates amount of steps simulation will iterate for
//    Vector3 directionVector = transform.forward; //INPUT launch direction (This Vector2 is automatically normalized for us, keeping it in low and communicable terms)
//    Vector3 launchPosition = transform.position; //INPUT launch origin (Important to make sure RayCast is ignoring some layers (easiest to use default Layer 2))

//    float _vel = _force / rb.mass * Time.fixedDeltaTime; //Initial Velocity, or Velocity Modifier, with which to calculate Vector Velocity

//    for (int i = 0; i < maxSteps; ++i)
//    {
//        //Remember f(t) = (x0 + x*t, y0 + y*t - 9.81t≤/2)
//        //calculatedPosition = Origin + (transform.up * (speed * which step * the length of a step);
//        Vector3 calculatedPosition = launchPosition + directionVector * _vel * i * timeStepInterval; //Move both X and Y at a constant speed per Interval
//        calculatedPosition.y += -gravity / 2 * Mathf.Pow(i * timeStepInterval, 2); //Subtract Gravity from Y

//        lineRendererPoints.Add(calculatedPosition); //Add this to the next entry on the list
//    }
//    return lineRendererPoints;
//}
//void DrawTrajectory(float force, LineRenderer line, Rigidbody rb)
//{
//    List<Vector3> points = SimulateArc(force, rb);
//    line.positionCount = points.Count;
//    line.SetPositions(points.ToArray());
//}


#endregion



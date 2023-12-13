using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using Yudiz.Drums.DrumPiece;
using Yudiz.Drums.Utilities;

namespace Yudiz.Drums.Player
{
    public class PlayerStick : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] XRGrabInteractable xrHands;
        [SerializeField] Rigidbody stickRB;        
        [SerializeField] Vector3 initialPosition;

        FixedJoint fixedJoint;        
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            xrHands = GetComponent<XRGrabInteractable>();

            xrHands.selectEntered.AddListener(Grab);
            xrHands.selectExited.AddListener(UnGrab);
        }

        private void OnDestroy()
        {
            xrHands.selectEntered.RemoveListener(Grab);
            xrHands.selectExited.RemoveListener(UnGrab);
        }

        /*public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision on Obj");
            var collideObj = collision.gameObject.GetComponent<DrumBeats>();
            if (collideObj != null)
            {
                Debug.Log("Collision not null");
                collideObj.CollisionHit(collideObj.drumType, collision);
            }
        }*/

      
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void Grab(SelectEnterEventArgs args0)
        {
          /*  Debug.Log("grabbing");
            
            fixedJoint = args0.interactorObject.transform.GetComponent<FixedJoint>();
            Invoke(nameof(AttachFixedJoint), 0.1f);*/
        }

        private void UnGrab(SelectExitEventArgs args0)
        {            
            /*var t = args0.interactorObject.transform.GetComponent<FixedJoint>();
            t.connectedBody = null;*/
            ResetStickPosition();
        }

        private void ResetStickPosition()
        {
            this.transform.localPosition = initialPosition;
            this.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

        /*private void AttachFixedJoint()
        {
            fixedJoint.connectedBody = stickRB;
            xrHands.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        }*/
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
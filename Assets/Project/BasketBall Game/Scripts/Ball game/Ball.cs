using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Yudiz.VRBasketBall.Core
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody _rb { get; private set; }
        private bool isColliding = false;
		private XRGrabInteractable grabInteractable;
		public bool isThrown { get; private set; }
        private void Start()
        {
            isThrown = false;
            _rb = GetComponent<Rigidbody>();
			grabInteractable = GetComponent<XRGrabInteractable>();
			grabInteractable.selectEntered.AddListener(GrabBall);
			grabInteractable.selectExited.AddListener(ReleaseBall);
		}
        public void OnDestroy()
        {
            StopAllCoroutines();
            //New ball spawn
           
        }
        #region XR Methods
        private void GrabBall(SelectEnterEventArgs arg0)
		{
            
			if (arg0.interactorObject is XRDirectInteractor)
			{
                if (!GameManager.instance.ballGrabbedForFirstTime)
                    GameManager.instance.FirstTimeBallGrabbed();
				ResetVelocity();
				arg0.interactorObject.transform.GetChild(0).gameObject.SetActive(false);
			}
		}
		private void ReleaseBall(SelectExitEventArgs arg0)
		{
			arg0.interactorObject.transform.GetChild(0).gameObject.SetActive(true);
		}
		#endregion
		private void OnCollisionEnter(Collision collision)
        {
            AudioManager.instance.SoundPlay(SoundName.BallBounce);
            if (collision.gameObject.name == "Plane")
            {
                if (!isColliding && isThrown)
                {
                    isColliding = true;
                    _rb.drag = 0.5f;
                    ResetBall();
                }
            }
        }
        public void ResetBall()
        {
            StartCoroutine(ResetRoutine());
            Events.onBallThrown?.Invoke();
        }
        IEnumerator ResetRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.drag = 0.1f;
            BallThrown(false);
            isColliding = false;
            gameObject.SetActive(false);
        }
        public void ResetVelocity()
        {
            _rb.velocity = Vector3.zero;
        }
        public void ActivateKinametic(bool activate)
        {
            if (activate == true)
                _rb.isKinematic = true;
            else
                _rb.isKinematic = false;
        }
        public bool BallThrown(bool value)
        {
            return isThrown = value;
        }
    }
}
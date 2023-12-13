using System;
using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Yudiz.VRBasketBall.Core
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private InputActionReference _leftprimaryButton;
		[SerializeField] private Transform playerPlacment;
		[SerializeField] private Transform CenterAirPoint;
		[SerializeField] private Sprite[] textImages;
		[SerializeField] private GameObject textPointObj;
		[SerializeField] private Animator lightAnim;
		[SerializeField] private Animator stripLightAnim;
		[SerializeField] private Ball ballObj;
		//[SerializeField] private Net net;


		//[SerializeField] private FadeScreen fadeScreen;
		[SerializeField] public bool ballGrabbedForFirstTime;//{ get; private set; }

		private List<Ball> balls = new List<Ball>();
		private int[] angles = new int[] {45,-45};
		private bool isGameOver = false;
		public bool isExitButtonPerformed;
		private Ball ball;
		private int currentPosIndex = -1;
		private int previousIndex = 0;
		private float currentAngle = 90;

		public static GameManager instance;

		public Transform centerPoint; // The center around which the object should rotate
		public float startAngle = 0f; // Starting angle in degrees
		public float endAngle = 360f; // Ending angle in degrees
		public float radius = 5f; // Radius of the circular orbit
		public float duration = 5f; // Duration of the lerp in seconds
		public int questionCont=0;
		public QuestionDatabase questionDatabase;

		private void Awake()
		{
			if (instance == null)
				instance = this;
		}
		private void OnEnable()
		{
			Events.onChangeActiveNet += RepositionNet;
			Events.onGameplayStart += Initialize;
			Events.onGameOver += DestroyBall;
			Events.onGameOver += ResetFirstTimeBallGrab;
			Events.onBallThrown += PlaceNextBall;
			//_leftprimaryButton.action.performed += OnExitButtonPerformed;
		}
		private void OnDisable()
		{
			Events.onChangeActiveNet -= RepositionNet;
			Events.onGameplayStart -= Initialize;
			Events.onGameOver -= DestroyBall;
			Events.onGameOver -= ResetFirstTimeBallGrab;
			Events.onBallThrown -= PlaceNextBall;
			//_leftprimaryButton.action.performed -= OnExitButtonPerformed;
		}
		private void Start()
		{
			transform.parent = playerPlacment;
			transform.position = Vector3.zero;
			lightAnim.gameObject.SetActive(false);
			ResetFirstTimeBallGrab();
		}
		private void RepositionNet()
		{
			currentAngle = startAngle;
			float calculatedAngle = startAngle + UnityEngine.Random.Range(-45,45);
			if(calculatedAngle > 135 || calculatedAngle < 45)
			{
				RepositionNet();
				return;
			}
			startAngle = calculatedAngle;
			//StartCoroutine(LerpOverTime(currentAngle, calculatedAngle, duration));
		}
		private void OnExitButtonPerformed(InputAction.CallbackContext obj)
		{
			if (!isExitButtonPerformed)
			{
				ViewController.Instance.ShowPopup(PopupName.ExitScreen);
				isExitButtonPerformed = true;
			}
			else
			{
				ViewController.Instance.HidePopup(PopupName.ExitScreen);
				isExitButtonPerformed = false;
			}
		}
		private void Initialize()
		{
			currentPosIndex = -1;
			startAngle = 90;
			isGameOver = false;
			//StartCoroutine(LerpOverTime(currentAngle, startAngle, 0.5f));
			for (int i = 0; i < 5; i++)
			{
				Ball b = Instantiate(ballObj);
				balls.Add(b);
				b.gameObject.SetActive(false);
			}
			if (ball == null)
			{
				ball = balls[0];
				balls.RemoveAt(0);
				SetBallPosition();
				ball.gameObject.SetActive(true);
			}
			else
			{
				SetBallPosition();
			}
			PlayLightAnimation();
		}
		public void PlaceNextBall()
		{
			StartCoroutine(SortingBalls());
		}
		IEnumerator SortingBalls()
		{
			Debug.Log("Soreting Ball");
			yield return new WaitForSeconds(0.5f);
			balls.Add(ball);
			ball = null;
			ball = balls[0];
			balls.RemoveAt(0);
			SetBallPosition();
			if(ball != null) 
				ball.gameObject.SetActive(true);
		}
		private void SetBallPosition()
		{
			if (ball != null)
			{
				Vector3 ballDirection = Camera.main.transform.forward;
				ballDirection.y = 0;
				Vector3 newPosition = Camera.main.transform.position + ballDirection * 0.7f;
				ball.transform.position = newPosition;
			}
		}
		private void DestroyBall()
		{
			Destroy(ball.gameObject);
			ball = null;
			foreach (Ball b in balls)
			{
				Destroy(b.gameObject);
			}
			balls.Clear();
			StopLightAnimation();
		}

		public int GetRandomIndex()
		{
			currentPosIndex = UnityEngine.Random.Range(0,angles.Length);
			if (currentPosIndex == previousIndex)
			{
				return GetRandomIndex();
			}
			else
			{
				previousIndex = currentPosIndex;
				return currentPosIndex;
			}
		}
		public void FirstTimeBallGrabbed() { ballGrabbedForFirstTime = true; }
		private void ResetFirstTimeBallGrab() { ballGrabbedForFirstTime = false; isGameOver = true;}
		
		#region Light Animations
		public void PlayLightAnimation()
		{
			lightAnim.gameObject.SetActive(true);
		}
		public void StopLightAnimation() {
			lightAnim.gameObject.SetActive(false);
		}
		public void PlayStripLightAnimation()
		{
			stripLightAnim.SetBool("PlayAnim", true);
		}
		public void StopStripLightAnimation()
		{
			stripLightAnim.SetBool("PlayAnim", false);
		}
		public void ShowTextImageAnimation(Transform spawnPos)
		{
			GameObject textObj = Instantiate(textPointObj, spawnPos.position, Quaternion.identity);
			Vector3 direction = textObj.transform.position - Camera.main.transform.position;
			Vector3 lookAtPoint = textObj.transform.position + direction;
			textObj.transform.LookAt(lookAtPoint);
			Sprite tex = textImages[UnityEngine.Random.Range(0, textImages.Length)];
			Image image = textObj.GetComponentInChildren<Image>();
			image.sprite = tex;
			/*MeshRenderer renderer = textObj.GetComponent<MeshRenderer>();
			Material mat = renderer.material;
			mat.SetTexture("_BaseMap", tex);
			mat.SetTexture("_EmissionMap", tex);*/
			//StartCoroutine(ShowTextAnimationRoutine(textObj.transform, mat, 2f));
			StartCoroutine(ShowTextCanvasAnimationRoutine(textObj.transform, image, 2f));
		}
		private IEnumerator ShowTextAnimationRoutine(Transform imagePos, Material mat, float duration)
		{
			Color c = mat.GetColor("_EmissionColor");
			float time = 0f;
			while (time < duration)
			{
				time += Time.deltaTime;
				imagePos.Translate(Vector3.up * Time.deltaTime);
				c.a = Mathf.Lerp(1, 0, time / duration);
				yield return null;
			}
			c.a = 0;
			Destroy(imagePos.gameObject);
		}	
		private IEnumerator ShowTextCanvasAnimationRoutine(Transform imagePos,Image image,float duration)
		{
			Color c = image.color;
			float time = 0f;
			while (time < duration)
			{
				time += Time.deltaTime;
				imagePos.Translate(Vector3.up * Time.deltaTime);
				c.a = Mathf.Lerp(1, 0, time / duration);
				image.color = c;
				yield return null;
			}
			c.a = 0;
			Destroy(imagePos.gameObject);
		}
		#endregion

		#region circular Motion
		//IEnumerator LerpOverTime(float startAngle, float endAngle, float duration, Action callback = null)
		//{
		//	yield return new WaitForSeconds(0.5f);
		//	float elapsedTime = 0f;

		//	while (elapsedTime < duration)
		//	{
		//		float fraction = elapsedTime / duration;
		//		Vector3 newPos = CalculateCircularPosition(startAngle, endAngle, fraction);
		//		net.transform.position = newPos;
		//		Vector3 direction = net.transform.position - CenterAirPoint.position;
		//		Vector3 lookAtPoint = net.transform.position + direction;
		//		net.transform.LookAt(lookAtPoint);
		//		elapsedTime += Time.deltaTime;
		//		yield return null;
		//	}
		//	//callback();
		//}

		//Vector3 CalculateCircularPosition(float startAngle, float endAngle, float fraction)
		//{
		//	// Convert angles from degrees to radians for Mathf functions
		//	float theta1 = startAngle * Mathf.Deg2Rad;
		//	float theta2 = endAngle * Mathf.Deg2Rad;

		//	// Interpolate the angle based on the fraction
		//	float interpolatedTheta = theta1 + fraction * (theta2 - theta1);

		//	// Calculate the x and y coordinates using the circle's parametric equations
		//	float x = centerPoint.position.x + radius * Mathf.Cos(interpolatedTheta);
		//	float z = centerPoint.position.z + radius * Mathf.Sin(interpolatedTheta);

		//	// Return the interpolated position
		//	return new Vector3(x, net.transform.position.y, z); // Assuming the motion is in the XY plane
		//}
		#endregion

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, radius);
		}
		[ContextMenu("Show Circular Motion")]
		public void ShowCiruclarMotion()
		{
			Events.onChangeActiveNet?.Invoke();
		}
#endif
	}
}
using UnityEngine;

namespace Yudiz.VRBasketBall.Core
{
	public class AttractObject : MonoBehaviour
	{
		[SerializeField] private Transform attractPoint;
		[SerializeField] private float speed = 0.15f;
		private bool inRadius;
		private Ball ball;

		private void OnTriggerEnter(Collider other)
		{
			ball = other.gameObject.GetComponent<Ball>();
			if (ball != null)
				inRadius = true;
		}
		private void OnTriggerStay(Collider other)
		{
			if (ball != null)
			{
				if (inRadius)
				{
					Vector3 direction = attractPoint.position - other.transform.position;
					ball._rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
				}
			}
		}
		private void OnTriggerExit(Collider other)
		{
			if (ball != null)
			{
				inRadius = false;
			}
		}
	}
}
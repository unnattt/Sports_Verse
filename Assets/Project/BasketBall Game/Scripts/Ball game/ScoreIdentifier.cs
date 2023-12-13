using UnityEngine;

namespace Yudiz.VRBasketBall.Core
{
	public class ScoreIdentifier : MonoBehaviour
	{
		public bool isBallPassed { get; private set; }
		void Start()
		{
			isBallPassed = false;
		}
		private void OnTriggerEnter(Collider other)
		{
			Ball ball = other.GetComponent<Ball>();
			if (ball != null)
			{
				isBallPassed = true;
			}
		}
		public void Reset()
		{
			isBallPassed = false;
		}
	}
}
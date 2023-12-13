using UnityEngine;
using Yudiz.VRBasketBall.Core;

public class BallThrowRangeIdentifier : MonoBehaviour
{
	private void OnTriggerExit(Collider other)
	{
		Ball ball = other.GetComponent<Ball>();
		if(ball != null )
		{
			Debug.Log("trigger exit");
			if(ball.isThrown == false)
			{
				ball.BallThrown(true);
				//Events.onBallThrown?.Invoke();
			}
		}
	}
}

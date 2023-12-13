using UnityEngine;
namespace Yudiz.VRBasketBall.Core
{
	public class Net : MonoBehaviour
	{
		[SerializeField] private ScoreIdentifier scoreIdentifier;
		private void EnableScoreIdentifier()
		{
			scoreIdentifier.gameObject.SetActive(true);
		}
		private void DisableScoreIdentifier()
		{
			scoreIdentifier.gameObject.SetActive(false);
		}
		public void DisableNet()
		{
			DisableScoreIdentifier();
		}
		public void EnableNet()
		{
			EnableScoreIdentifier();
		}
	}
}
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRBasketBall.Core
{
	public class Basket : MonoBehaviour
	{
		[SerializeField] private Animator netAnim;
		[SerializeField] private ScoreIdentifier firstLayerTrigger;
		[SerializeField] private Transform textSpawnPos;
		[SerializeField] private List<ParticleSystem> ParticleObj;
		[SerializeField] private Material spaceShipMat;

		private void OnTriggerEnter(Collider other)
		{
			Ball ball = other.GetComponent<Ball>();
			if (ball != null)
			{
				if (firstLayerTrigger.isBallPassed)
				{
					netAnim.SetTrigger("Basket");
					foreach (ParticleSystem p in ParticleObj)
					{
						p.Play();
					}
					GameManager.instance.ShowTextImageAnimation(textSpawnPos);
					GameManager.instance.PlayStripLightAnimation();
					AudioManager.instance.SoundPlay(SoundName.Goal);
					Events.onTimeAdded?.Invoke(5);
					Events.onScoreAdd?.Invoke(1);
					firstLayerTrigger.Reset();
				}
			}
		}

#if UNITY_EDITOR
		[ContextMenu("ShowTextImage")]
		public void ShowTextImage()
		{
			GameManager.instance.ShowTextImageAnimation(textSpawnPos);
		}
#endif
	}
}

using UISystem;
using UnityEngine;
using Yudiz.VRBasketBall.UI;

namespace Yudiz.VRBasketBall.Core
{
	public class Timer : MonoBehaviour
	{
		[SerializeField] private float initialTime = 10;

		private AudioSource timerAudioSource;

		public float tempTime { get; private set; }
		bool timeOver = true;

		#region Unity_CALLBACKS
		private void OnEnable()
		{
			Events.onGameplayStart += ActivateTime;
			Events.onTimeAdded += AddTime;
		}
		private void OnDisable()
		{
			Events.onGameplayStart -= ActivateTime;
			Events.onTimeAdded -= AddTime;
		}
		private void Start()
		{
			timerAudioSource = GetComponent<AudioSource>();
		}
		string value;
		private void Update()
		{
			if (!timeOver && GameManager.instance.ballGrabbedForFirstTime)
			{
				tempTime -= Time.deltaTime;
				int seconds = Mathf.FloorToInt(tempTime % 60);
				int milliseconds = Mathf.FloorToInt((tempTime * 100) % 100);

				value = string.Format("{0:00}:{1:00}", seconds, milliseconds);

				ViewController.Instance.GetScreen<GameplayScreen>(ScreenName.GamePlayScreen).SetTimerText(value);
				if(tempTime <= 5f)
				{
					if(!timerAudioSource.isPlaying)
						timerAudioSource.Play();
				}
				else
				{
					if(timerAudioSource.isPlaying)
						timerAudioSource.Stop();
				}
				if (tempTime < 0)
				{
					if (timerAudioSource.isPlaying)
						timerAudioSource.Stop();
					StopTimer();
                    //Events.onGameOver?.Invoke();
                    //ViewController.Instance.ChangeView(ScreenName.GameOverScreen);
                    //Debug.Log("GAME OVER");
                }
			}
		}
		#endregion
		private void AddTime(int timeAmount)
		{
			tempTime += (float)timeAmount;
		}
		private void ActivateTime()
		{
			tempTime = initialTime;
			int seconds = Mathf.FloorToInt(tempTime % 60);
			int milliseconds = Mathf.FloorToInt((tempTime * 100) % 100);

			value = string.Format("{0:00}:{1:00}", seconds, milliseconds);

			ViewController.Instance.GetScreen<GameplayScreen>(ScreenName.GamePlayScreen).SetTimerText(value);
			timeOver = false;
		}
		private void StopTimer()
		{
			timeOver = true;
		}
		public bool Pausetimer(bool pause)
		{
			return timeOver = pause;
		}
	}
}
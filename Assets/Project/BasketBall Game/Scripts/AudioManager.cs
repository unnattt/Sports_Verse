using UnityEngine;

namespace Yudiz.VRBasketBall.Core
{
	public class AudioManager : MonoBehaviour
	{
		public Sound[] clips;
		public static AudioManager instance;

		AudioSource audioSource;

		public void Awake()
		{
			if (instance == null)
				instance = this;
			audioSource = GetComponent<AudioSource>();
		}
		public void SoundPlay(SoundName name)
		{
			foreach (var item in clips)
			{
				if (item.name == name)
				{
					audioSource.PlayOneShot(item.clip);
					break;
				}
			}
		}
		public void SoundMute(bool val)
		{
			audioSource.mute = val;
		}
	}
		[System.Serializable]
		public class Sound
		{
			public SoundName name;
			public AudioClip clip;
		}
		public enum SoundName
		{
			None,
			Goal,
			BallBounce,
			Timer,
		}
}
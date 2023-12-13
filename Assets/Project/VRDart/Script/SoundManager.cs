using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yudiz.VRDart.Manager
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        public AudioSource loopaudioSource;


        public Sound[] clips;
        public Voice[] voice;
        public PlayerName[] player;


        public static SoundManager inst;

        public void Awake()
        {
            inst = this;
            loopaudioSource.Play();
            //loopaudioSource.volume = 0.03f;

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

        public void VoiceSoundPlay(VoiceSound name)
        {
            foreach (var item in voice)
            {
                if (item.voicename == name)
                {
                    audioSource.PlayOneShot(item.clip);
                    break;
                }
            }

        }
        public void PlayerNameSoundPlay(PlayerNameSound name)
        {
            foreach (var item in player)
            {
                if (item.Playername == name)
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
        [System.Serializable]
        public class Sound
        {
            public SoundName name;
            public AudioClip clip;
        }

        [System.Serializable]
        public class Voice
        {
            public VoiceSound voicename;
            public AudioClip clip;
        }

        [System.Serializable]
        public class PlayerName
        {
            public PlayerNameSound Playername;
            public AudioClip clip;
        }
        public enum SoundName
        {
            BG,
            Click,
            Panel,
            DartHit,

        }

        public enum VoiceSound
        {
            MissShort,
            Good,
            CloseOne,
            excellent,

        }

        public enum PlayerNameSound
        {
            Player_1,
            Player_2,
            Player_3,
            Player_4,

        }
    }
}

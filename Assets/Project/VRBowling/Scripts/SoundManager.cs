using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRBowling
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        // public AudioSource loopaudioSource;


        public Sound[] clips;
        public Voice[] voice;
        public Round[] rounds;


        public static SoundManager inst;

        public void Awake()
        {
            inst = this;
            //loopaudioSource.Play();
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
        public void CurrentRounds(RoundSound name)
        {
            foreach (var item in rounds)
            {
                if (item.Roundname == name)
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
        public class Round
        {
            public RoundSound Roundname;
            public AudioClip clip;
        }
        public enum SoundName
        {
            BG,
            Click,
            Panel,
            BallIouch,
            Trunchange,


        }

        public enum VoiceSound
        {
            MissShort,
            Sticke,
            square,
            PleaseWait,

        }

        public enum RoundSound
        {
            Round_1,
            Round_2,
            Round_3,
            Round_4,
            Round_5,

        }
    }
}


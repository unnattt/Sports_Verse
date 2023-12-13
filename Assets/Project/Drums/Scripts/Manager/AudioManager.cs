using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using Yudiz.Drums.Utilities;

namespace Yudiz.Drums.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region PUBLIC_VARS
        public List<AudioData> audioList;
        public AudioSource audioSource;
        //public AudioSource bgSource;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS


        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void PlaySound(SoundName soundName)
        {
            var t = audioList.Find(a => a.audioName == soundName);
            if (t != null)
            {
                audioSource.PlayOneShot(t.audioClip);
            }
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }

    [System.Serializable]
    public class AudioData
    {
        public SoundName audioName;
        public AudioClip audioClip;
    }
}
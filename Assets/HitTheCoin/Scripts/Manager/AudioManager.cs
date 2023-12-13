using System.Collections.Generic;
using UnityEngine;

using Yudiz.MiniGame.Base;

namespace Yudiz.MiniGame.Manager 
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region PUBLIC_VARS
        public List<AudioData> audioList;
        public AudioSource audioSource;

        public AudioSource bgSource;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS        
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void PlaySound(AudioName name)
        {
            var audioName = audioList.Find(a => a.audioName == name);
            if(audioName != null)
            {
                audioSource.PlayOneShot(audioName.audioClip);
            }
        }

        public void PlayBGMusic()
        {
            bgSource.Play();
            bgSource.loop = true;
        }

        public void StopBGMusic()
        {
            bgSource.Stop();
            bgSource.loop = false;  
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
        public AudioName audioName;
        public AudioClip audioClip;
    }

    public enum AudioName
    {
        Hammering,
        CoinPop,
    }
}
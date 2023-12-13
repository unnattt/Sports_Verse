using System.Collections.Generic;
using UnityEngine;
using Yudiz.BaseFramework;
using Yudiz.Drums.Utilities;

namespace Yudiz.Drums.Manager
{
    public class VFXManager : Singleton<VFXManager>
    {
        #region PUBLIC_VARS
        public List<VFXData> vfxList;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void PlayParticle(ParticleName name)
        {
            var t = vfxList.Find(a => a.particleName == name);
            if (t != null)
            {
                var playingParticle =  Instantiate(t.particleSystem, t.particleSpawnPoint);
                playingParticle.Play();
                Destroy(playingParticle.gameObject, playingParticle.main.duration);
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
    public class VFXData
    {
        public ParticleName particleName;
        public ParticleSystem particleSystem;
        public Transform particleSpawnPoint;
    }
}
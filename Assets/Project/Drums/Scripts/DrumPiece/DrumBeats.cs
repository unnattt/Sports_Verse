using UnityEngine;
using DG.Tweening;

using Yudiz.Drums.Utilities;
using Yudiz.Drums.Manager;
using Yudiz.Drums.Player;

namespace Yudiz.Drums.DrumPiece
{
    public class DrumBeats : MonoBehaviour
    {
        #region PUBLIC_VARS
        public DrumTypes drumType;
        #endregion

        #region PRIVATE_VARS
        [Header("Tweening Scaling Effect")]
        [SerializeField] Vector3 changeSize, origSize;
        [SerializeField] float minChangeSize;

        [Header("Tweening Rotation Effect")]
        [SerializeField] float duration;
        [SerializeField] float strength;
        [SerializeField] int vibration;
        [SerializeField] float randomness;
        [SerializeField] bool fading;

        #endregion

        #region UNITY_CALLBACKS
        //private void OnCollisionEnter(Collision collision)
        //{
        //    Debug.Log("Collision");
        //    Debug.Log("Collided Object " + collision.collider.name);
        //    var t = collision.gameObject.GetComponent<DrumBeats>();
        //    if (t != null)
        //    {
        //        Debug.Log("collision hapened");
        //        GetCollidedObjectSound(t.drumType, collision);
        //    }
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerStick playerstick))
            {
                CollisionHit(drumType, null);
            }
        }

        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void CollisionHit(DrumTypes drum, Collision collision)
        {
            GetCollidedObjectSound(drum, collision);
        }
        #endregion

        #region PRIVATE_FUNCTIONS
        private void GetCollidedObjectSound(DrumTypes drum, Collision collision)
        {
            switch (drum)
            {
                case DrumTypes.Base:
                    Debug.Log("Base Drum Called");
                    PlayParticularEffect(SoundName.BassDrum, ParticleName.BaseDrumPS, collision, drum);
                    break;

                case DrumTypes.CrashCymbal:
                    Debug.Log("Crash Cymbal Called");
                    PlayParticularEffect(SoundName.CrashCymbal, ParticleName.CrashCymbalPS, collision, drum, true);
                    break;

                case DrumTypes.RideCymbal:
                    Debug.Log("Ride Cymbal Called");
                    PlayParticularEffect(SoundName.RideCymbal, ParticleName.RideCymbalPS, collision, drum, true);
                    break;

                case DrumTypes.FloorTom:
                    Debug.Log("Floor Tom Called");
                    PlayParticularEffect(SoundName.FloorTom, ParticleName.FloorTomPS, collision, drum, true);
                    break;

                case DrumTypes.HighTom:
                    Debug.Log("High Tom Called");
                    PlayParticularEffect(SoundName.HighTom, ParticleName.HighTomPS, collision, drum, true);
                    break;

                case DrumTypes.HiHats:
                    Debug.Log("Hi Hats Called");
                    PlayParticularEffect(SoundName.HiHats, ParticleName.HiHatsPs, collision, drum, true);
                    break;

                case DrumTypes.MediumTom:
                    Debug.Log("Medium Tom Called");
                    PlayParticularEffect(SoundName.MediumTom, ParticleName.MediumTomPS, collision, drum, true);
                    break;

                case DrumTypes.SnareDrum:
                    Debug.Log("Snare Drum Called");
                    PlayParticularEffect(SoundName.SnareDrum, ParticleName.snareDrumPS, collision, drum, true);
                    break;
            }
        }

        private void PlayParticularEffect(SoundName soundName, ParticleName particleName, Collision collision, DrumTypes type, bool isScalingOrRotation = false, int maxCollisionForce = 0)
        {
            if (isScalingOrRotation)
            {

                AudioManager.Instance.PlaySound(soundName);
                VFXManager.Instance.PlayParticle(particleName);
                TweeningRotationEffect(type);

            }
            else
            {
                Debug.Log("Particular Else Called");
                AudioManager.Instance.PlaySound(soundName);
                VFXManager.Instance.PlayParticle(particleName);
                TweeningScalingEffect(type);
            }
        }

        private void TweeningScalingEffect(DrumTypes currentType)
        {
            if (drumType == currentType)
            {
                Debug.Log("Scaling Method Called");
                this.transform.DOScale(changeSize, minChangeSize).OnComplete(() => { this.transform.DOScale(origSize, minChangeSize); });
            }
        }

        private void TweeningRotationEffect(DrumTypes currentType)
        {
            if (drumType == currentType)
            {
                Debug.Log("Rotation Method Called");
                Tweener tween;
                if (currentType == DrumTypes.CrashCymbal || currentType == DrumTypes.RideCymbal || currentType == DrumTypes.HiHats)
                {
                    tween = this.transform.DOShakeRotation(duration, strength, vibration, randomness, fadeOut: fading).OnComplete(() => { this.transform.DORotate(origSize, minChangeSize); });

                }
                else
                {
                    tween = this.transform.DOShakeRotation(duration, strength, vibration, randomness, fadeOut: fading);
                }
                if (tween.IsPlaying()) return;
                this.transform.DOKill();
            }
        }
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
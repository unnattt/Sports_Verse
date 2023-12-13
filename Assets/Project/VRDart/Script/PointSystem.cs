using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Yudiz.VRDart.DartController;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.PointManager
{
    public class PointSystem : MonoBehaviour
    {

        public int scorePoint;
        public int scoreBounce;
        public Color color;
        public Transform pointPos;
        public GameObject pointObj;
        private Material _mat;

        public async void OnCollisionEnter(Collision collision)
        {
            SoundManager.inst.SoundPlay(SoundManager.SoundName.DartHit);
            DartMovement dart = collision.gameObject.GetComponentInParent<DartMovement>();
            _mat = this.gameObject.GetComponent<Renderer>().material;

            if (dart && !dart.isThrowed)
            {
                color = _mat.GetColor("_EmissionColor");
                dart.isThrowed = true;
                _mat.DOColor(Color.white, "_EmissionColor", 0.8f).SetEase(Ease.InOutSine).SetLoops(6, LoopType.Yoyo).OnComplete(() =>
                {
                    _mat.SetColor("_EmissionColor", color);

                });
                var FinalScore = scorePoint * scoreBounce;
                ShowGainPoint(FinalScore);

                if (FinalScore >= 5 && FinalScore <= 25)
                {
                    SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.Good);
                }
                else if (FinalScore > 26 && FinalScore <= 50)
                {
                    SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.CloseOne);
                }
                else if (FinalScore > 51 && FinalScore <= 100)
                {
                    SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.excellent);
                }
                else
                {
                    SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.MissShort);
                }


                GameManager._instance.AddScore(FinalScore);

            }


        }


        public void ShowGainPoint(int value)
        {
            GameObject point = Instantiate(pointObj, pointPos.position, Quaternion.identity, pointPos);
            TextMeshPro point_text = point.GetComponent<TextMeshPro>();
            point_text.text = "+" + value.ToString();
            point_text.fontSize = 5;
            point_text.alignment = TextAlignmentOptions.Center;
            StartCoroutine(ShowGainPointAnimation(point_text, 1f));
        }

        private IEnumerator ShowGainPointAnimation(TextMeshPro text, float duration)
        {
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                text.transform.Translate(Vector3.up * Time.deltaTime);
                text.alpha = Mathf.Lerp(1, 0, time / duration);
                yield return null;
            }
            text.alpha = 0;
            yield return new WaitForSeconds(0.2f);
            Destroy(text.gameObject);
        }


    }
}

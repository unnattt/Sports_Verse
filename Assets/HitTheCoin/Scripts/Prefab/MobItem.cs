using UnityEngine;

using Yudiz.MiniGame.Player;
using Yudiz.MiniGame.Manager;

namespace Yudiz.MiniGame.Prefab
{
    public class MobItem : MonoBehaviour
    {
        #region PUBLIC_VARS
        public ItemState state;
        #endregion

        #region PRIVATE_VARS
        [SerializeField] ParticleSystem coinCollectionPS;
        private Quaternion particleRotation;
        private Vector3 particleScale;
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            particleRotation = Quaternion.Euler(90, 0, 0);
            particleScale = Vector3.one * 0.2f;
        }
        private void OnEnable()
        {
            state = ItemState.CanHit;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    var collideObj = other.gameObject.GetComponent<PlayerItem>();
        //    if(collideObj != null && state != ItemState.Missed)
        //    {
        //        Debug.Log("Object Hit");
        //        CoinCollect();
        //    }
        //}
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        public void Hide()
        {
            Destroy(this.gameObject);
        }

        public void CoinCollect()
        {
            AudioManager.Instance.PlaySound(AudioName.Hammering);
            Hide();
            var particle = Instantiate(coinCollectionPS, transform.position, particleRotation);
            particle.transform.localScale = particleScale;
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);
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

    public enum ItemState
    {
        CanHit,
        Missed,
    }
}
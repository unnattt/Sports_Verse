using UnityEngine;
using TMPro;

using Yudiz.ShootingGame.Manager;

namespace Yudiz.ShootingGame.UI
{
    public class ScoreUI : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        [SerializeField] TMP_Text scoreTxt;
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            scoreTxt.text = ScoreManager.Instance.score.ToString();
            ScoreManager.Instance.ShowScore += UpdateScoreTxt;
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            ScoreManager.Instance.ShowScore -= UpdateScoreTxt;
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS
        private void UpdateScoreTxt(int score)
        {
            scoreTxt.text = score.ToString();
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
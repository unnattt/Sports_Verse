using UnityEngine;
using TMPro;
using Yudiz.VRBasketBall.Core;
using DG.Tweening;

namespace Yudiz.VRBasketBall.UI
{
    public class GameplayScreen : UISystem.Screen
    {
		#region PUBLIC_VARS
		#endregion

		#region PRIVATE_VARS
		[SerializeField] private ScoreHandler scoreHandler;
		[SerializeField] private TextMeshProUGUI TimerText;
		[SerializeField] private TextMeshProUGUI scoreText;
		[SerializeField] private TextMeshProUGUI highscoreText;

		private float uiOffset = 18f;
		private float canvasScale = 0.02f;
		#endregion

		#region UNITY_CALLBACKS
	/*	private void Update()
		{
			if(canvas.enabled)
			{
				SetCanvasPosition();
			}
		}*/
		#endregion

		#region PUBLIC_METHODS
		public void SetTimerText(string time)
		{
			TimerText.text = time;
		}
		#endregion

		#region PRIVATE_METHODS
		private void SetScoreText(int value)
		{
			scoreText.text = value.ToString();
		}
		private void SetHighScoreText(int value)
		{
			highscoreText.text = value.ToString();
		}
		#endregion

		#region BASE_UI_CALLBACKS
		public override void Show()
        {
			transform.localScale = Vector3.zero;
            Events.onScoreAdded += SetScoreText;
			Events.onHighscoreChanged += SetHighScoreText;
			highscoreText.text = scoreHandler.gamedata.highscore.ToString();
			scoreText.text = "0";
			transform.DOScale(canvasScale, 1f);
			base.Show();
        }
		public override void Hide()
        {
			Events.onScoreAdded -= SetScoreText;
			Events.onHighscoreChanged -= SetHighScoreText;
			transform.DOScale(0, 1f);
			base.Hide();
        }
        public override void Disable()
        {
            base.Disable();
        }
        public override void Redraw()
        {
            base.Redraw();
        }
        public override void Back()
        {
            base.Back();
        }
        #endregion
    }
}

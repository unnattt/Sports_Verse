using DG.Tweening;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.VRBasketBall.Core;

namespace Yudiz.VRBasketBall.UI
{
	public class GameOverScreen : UISystem.Screen
	{
		#region PUBLIC_VARS
		#endregion

		#region PRIVATE_VARS
		[SerializeField] private ScoreHandler scoreHandler;
		[SerializeField] private Button replay_Btn;
		[SerializeField] private Button menu_Btn;
		[SerializeField] private TextMeshProUGUI highscoreText;
		[SerializeField] private TextMeshProUGUI scoreText;

		private float uiOffset = 0.65f;
		private float canvasScale = 0.001f;
		#endregion

		#region UNITY_CALLBACKS
		/*	private void Update()
			{
				if (canvas.enabled)
					SetCanvasPosition();
				else
					return;
			}*/
		#endregion

		#region PUBLIC_METHODS
		#endregion

		#region PRIVATE_METHODS
		private void OnReplayBtnClicked()
		{
			replay_Btn.interactable = false;
			transform.DOScaleY(0, 0.5f)
			.OnComplete(() => {
				ViewController.Instance.ShowPopup(PopupName.ChooseQuestionsCatogoryScreen);
				//ViewController.Instance.ChangeView(ScreenName.GamePlayScreen); Events.onGameplayStart?.Invoke();
			});
		}
		private void OnQuitBtnClicked()
		{
			transform.DOScale(0, 1f)
			.SetEase(Ease.InBounce) // Use OutBounce easing for the bouncy effect
			.SetUpdate(true) // Ensure that the tween updates even when Time.timeScale is 0
			.SetLoops(1)
			.OnComplete(() => { Application.Quit(); });
		}
		private void SetCanvasPosition()
		{
			Vector3 uiDirection = Camera.main.transform.forward;
			uiDirection.y = 0;
			Vector3 newPosition = Camera.main.transform.position + uiDirection * uiOffset;
			//transform.position = Camera.main.transform.position + offset; 
			//transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * alignmentSpeed);
			transform.position = newPosition;
			Vector3 direction = transform.position - Camera.main.transform.position;
			Vector3 lookAtPoint = transform.position + direction;

			transform.LookAt(lookAtPoint);
		}
		#endregion

		#region BASE_UI_CALLBACKS
		public override void Show()
		{
			base.Show();
			transform.localScale = Vector3.zero;
			transform.DOScale(new Vector3(0.003f,0.003f,0.003f),0.3f);
			//transform.DOScale(canvasScale,1f)
			//.SetEase(Ease.OutBounce) // Use OutBounce easing for the bouncy effect
			//.SetUpdate(true) // Ensure that the tween updates even when Time.timeScale is 0
			//.SetRelative(false) // Use absolute values for scaling
			//.SetEase(Ease.OutBounce) // Use bounce easing
			//.SetLoops(1); // Set the number of loops (1 for no looping
			//SetCanvasPosition();
			highscoreText.text = scoreHandler.gamedata.highscore.ToString();
			scoreText.text = scoreHandler.totalScore.ToString();
			replay_Btn.interactable = true;
			replay_Btn.onClick.AddListener(OnReplayBtnClicked);
			menu_Btn.onClick.AddListener(OnQuitBtnClicked);
		}
		public override void Hide()
		{
			transform.DOScale(Vector3.zero, 0.3f);
			//transform.DOScale(0, 1f)
			//.SetEase(Ease.InBounce) // Use OutBounce easing for the bouncy effect
			//.SetUpdate(true) // Ensure that the tween updates even when Time.timeScale is 0
			//.SetRelative(false) // Use absolute values for scaling
			//.SetEase(Ease.InBounce) // Use bounce easing
			//.SetLoops(1); // Set th
			base.Hide();
			replay_Btn.onClick.RemoveAllListeners();
			menu_Btn.onClick.RemoveAllListeners();
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
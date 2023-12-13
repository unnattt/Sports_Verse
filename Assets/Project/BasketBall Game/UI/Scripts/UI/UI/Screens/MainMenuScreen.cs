using UnityEngine;
using UnityEngine.UI;
using UISystem;
using Yudiz.VRBasketBall.Core;
using DG.Tweening;

namespace Yudiz.VRBasketBall.UI
{
    public class MainMenuScreen : UISystem.Screen
    {
		#region PUBLIC_VARS
		#endregion

		#region PRIVATE_VARS
		private float canvasScale = 0.001f;
		private float uiOffset = 0.6f;
		private float alignmentSpeed = 1f;
		[SerializeField] private Button play_Btn;
		#endregion

		#region UNITY_CALLBACKS
		private void Start()
		{
			SetCanvasPosition();
		}
		#endregion

		#region PUBLIC_METHODS
		public void OnPlayButtonClicked()
        {
            ViewController.Instance.ChangeView(ScreenName.GamePlayScreen);
        }
		#endregion

		#region PRIVATE_METHODS
		private void OnPlayBtnClicked()
		{
			play_Btn.interactable = false;
			transform.DOScaleY(0, 0.5f)
			.OnComplete(() =>
			{
				ViewController.Instance.ShowPopup(PopupName.ChooseQuestionsCatogoryScreen);
				//ViewController.Instance.ChangeView(ScreenName.GamePlayScreen);
				//Events.onGameplayStart?.Invoke();
			});
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
			SetCanvasPosition();
			transform.localScale = Vector3.zero;
			transform.DOScale(canvasScale, 0.5f)
			.SetEase(Ease.OutBounce) // Use OutBounce easing for the bouncy effect
			.SetUpdate(true) // Ensure that the tween updates even when Time.timeScale is 0
			.SetRelative(false) // Use absolute values for scaling
			.SetEase(Ease.OutBounce) // Use bounce easing
			.SetLoops(1); // Set the number of loops (1 for no looping
			play_Btn.interactable = true;
			play_Btn.onClick.AddListener(OnPlayBtnClicked);
			base.Show();
		}
        public override void Hide()
        {
			base.Hide();
			play_Btn.onClick.RemoveAllListeners();
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

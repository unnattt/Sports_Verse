using UISystem;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.VRBasketBall.Core;

namespace Yudiz.VRBasketBall.UI
{
    public class ExitScreen : Popup
    {
       [SerializeField] private Button ExitButton;
       [SerializeField] private Button MenuButton;
       [SerializeField] private Transform leftHandController;

        private Camera vrCamera;
        private Vector3 offSet = new Vector3(0f, 0.5f, 0f);

        private void Start()
        {
            if (vrCamera == null)
                vrCamera = Camera.main;
            ExitButton.onClick.AddListener(() => { Application.Quit(); });
            MenuButton.onClick.AddListener(() =>
            {
				GameManager.instance.isExitButtonPerformed = false;
				Hide();
			});
        }
		private void Update()
        {
            if (canvas.enabled == true)
            {
                Vector3 difference = transform.position - vrCamera.transform.position;
                Vector3 lookAtPoint = transform.position + difference;

                transform.LookAt(lookAtPoint);
                transform.position = leftHandController.position + offSet;
            }
        }
        private void OnDestroy()
        {
            ExitButton.onClick.RemoveAllListeners();
            MenuButton.onClick.RemoveAllListeners();
        }
        public override void Show()
        {
            Time.timeScale = 0f;
            base.Show();
        }
        public override void Hide()
        {
            Time.timeScale = 1f;
            base.Hide();
        }
    }
}
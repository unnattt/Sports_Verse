using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Yudiz.VRBowling
{
    public class MainMenuScreen : MonoBehaviour
    {
        public Canvas mainManuScreen;
        private float uiOffset = 0.5f;


        // Start is called before the first frame update
        void Awake()
        {
            mainManuScreen = GetComponent<Canvas>();
            mainManuScreen.enabled = true;

        }
        public void Start()
        {
            SetCanvasPosition();
        }

        private async void SetCanvasPosition()
        {
            await Task.Delay(500);
            Vector3 uiDirection = Camera.main.transform.forward;
            uiDirection.y = 0;
            Vector3 newPosition = Camera.main.transform.position + uiDirection * uiOffset;

            //transform.position = Camera.main.transform.position + offset; 
            //transform.position = Vector3.Lerp(transform.position, newPosition,Time.deltaTime * alignmentSpeed);
            transform.position = newPosition;
            Vector3 direction = transform.position - Camera.main.transform.position;
            Vector3 lookAtPoint = transform.position + direction;
            transform.LookAt(lookAtPoint);
        }
    }
}

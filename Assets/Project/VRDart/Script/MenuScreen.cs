using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.UI
{
    public class MenuScreen : MonoBehaviour
    {

        public Canvas gameSelectionScreen;

        private Canvas _menuScreen;
        private float uiOffset = 0.5f;
        private float alignmentSpeed = 1f;
        // Start is called before the first frame update
        void Start()
        {
            _menuScreen = GetComponent<Canvas>();
            SetCanvasPosition();
        }

        // Update is called once per frame

        private void LateUpdate()
        {
            //if (_menuScreen.enabled == true)
            //{
            //    SetCanvasPosition();
            //}
            //else
            //{
            //    return;
            //}
        }


        public void Play()
        {
            SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
            _menuScreen.enabled = false;
            gameSelectionScreen.enabled = true;
        }

        private void SetCanvasPosition()
        {
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

        public void Show()
        {
            _menuScreen.enabled = true;
        }
    }
}


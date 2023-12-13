using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.VRDart.UI
{
    public class AlartScreen : MonoBehaviour
    {
        public Canvas _alartScreenCanvas;
        private float uiOffset = 0.5f;
        private float alignmentSpeed = 1f;

        void Update()
        {
            if (_alartScreenCanvas.enabled == true)
            {
                SetCanvasPosition();
            }
            else
            {
                return;
            }
        }

        private void SetCanvasPosition()
        {
            Vector3 uiDirection = Camera.main.transform.forward;
            uiDirection.y = 0;
            Vector3 newPosition = Camera.main.transform.position + uiDirection * uiOffset;
            //transform.position = Camera.main.transform.position + offset; 
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * alignmentSpeed);
            //transform.position = newPosition;
            Vector3 direction = transform.position - Camera.main.transform.position;
            Vector3 lookAtPoint = transform.position + direction;
            transform.LookAt(lookAtPoint);
        }

    }
}


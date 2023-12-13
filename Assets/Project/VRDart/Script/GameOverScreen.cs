using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.UI
{

    public class GameOverScreen : MonoBehaviour
    {

        public Canvas _gameOverScreen;
        private float uiOffset = 0.5f;
        private float alignmentSpeed = 1f;
        public GameObject ListofPlayerFirst;
        public TMP_Text winnerName;
        // Start is called before the first frame update
        void Start()
        {
            _gameOverScreen = GetComponent<Canvas>();
            SetCanvasPosition();

        }

        // Update is called once per frame
        void Update()
        {
            //if (_gameOverScreen.enabled == true)
            //{
            //    SetCanvasPosition();
            //}
            //else
            //{
            //    return;
            //}
        }


        public void Home()
        {
            SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
            //SceneManager.LoadScene(0);
            //  SoundManager.inst.loopaudioSource.Play();
            GameManager._instance.BackToMainMenu();
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
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Yudiz.VRDart.Manager;

namespace Yudiz.VRDart.UI
{

    public class GameSelectionScreen : MonoBehaviour
    {

        public Canvas _gameSelectionScreen;
        private float uiOffset = 0.5f;
        private float alignmentSpeed = 1f;

        public ToggleGroup playerGroup;
        public ToggleGroup gameModeGroup;

        // Start is called before the first frame update

        public void Awake()
        {
            //_gameSelectionScreen = GetComponent<Canvas>();
        }
        void Start()
        {
            //GetSelectedPlayerToggleIndex();
            SetCanvasPosition();
        }

        // Update is called once per frame
        void Update()
        {
            //if (_gameSelectionScreen.enabled == true)
            //{
            //    SetCanvasPosition();
            //}
            //else
            //{
            //    return;
            //}
        }

        public void PlayGame()
        {
            GetSelectedPlayerToggleIndex();
            GetSelectedGameModeToggleIndex();

            //SoundManager.inst.loopaudioSource.Stop();

        }


        public void GetSelectedPlayerToggleIndex()
        {
            Toggle[] toggles = playerGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    //return i;

                    if (i == 0)
                    {

                        GameManager._instance.PlayerCount = i + 2;
                    }
                    else if (i == 1)
                    {
                        Debug.Log(i);
                        SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
                        GameManager._instance.PlayerCount = i + 2;
                    }
                    else if (i == 2)
                    {
                        GameManager._instance.PlayerCount = i + 2;
                    }
                    GameManager._instance.TotalPlayerCount = GameManager._instance.PlayerCount;




                }
            }

        }

        public void OnToggalClickSound()
        {
            SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
        }
        public void GetSelectedGameModeToggleIndex()
        {
            Toggle[] toggles = gameModeGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                {
                    GameManager._instance.GameType(i);
                }
            }
            SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
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

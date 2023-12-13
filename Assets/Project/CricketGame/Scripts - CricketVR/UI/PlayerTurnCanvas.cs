using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Yudiz.VRCricket.Core;

namespace Yudiz.VRCricket.UI
{
    public class PlayerTurnCanvas : BaseScreen
    {

        [SerializeField] TextMeshProUGUI PlayerTurnText;
        [SerializeField] Button startbtn;
        [SerializeField] Button nextOption;
        [SerializeField] Button previosOption;
        [SerializeField] TMP_InputField stanceInputField;

        [SerializeField] private Transform playerTurnCanvas;

        private const string righthanded = "Right Handed";
        private const string lefthanded  = "Left Handed";
        private const string playersTurn = ", Ready To Roll !";

        private void OnEnable()
        {
            GameEvents.setDataForPlayerTurnScreen += SetPlayerName;
        }

        private void OnDisable()
        {
            GameEvents.setDataForPlayerTurnScreen -= SetPlayerName;
        }

        private void Start()
        {
            startbtn.onClick.AddListener(StartGame);
            nextOption.onClick.AddListener(NextOptionBtn);
            previosOption.onClick.AddListener(NextOptionBtn);

            stanceInputField.text = righthanded;
        }

        public void SetPlayerName()
        {
            PlayerTurnText.text = ScoreManager.inst.currentplayerName + playersTurn;

            Animate();
        }

        public void Animate()
        {
            AnimationManager.instance.ScaleTransform(playerTurnCanvas, 1.5f, 0);
        }

        public void StartGame()
        {
            OnStartGame();

            if (stanceInputField.text == righthanded)
            {
                GameEvents.rightStance?.Invoke();
            }
            else
            {
                GameEvents.leftStance?.Invoke();
            }
        }

        public void NextOptionBtn()
        {
            if(stanceInputField.text == righthanded)
            {
                Debug.Log("Changed to Left");
                stanceInputField.text = lefthanded;
            }
            else
            {
                Debug.Log("Changed to Right");
                stanceInputField.text = righthanded;
            }
        }

        private void OnStartGame()
        {
            ScreenManager.instance.SwitchScreen(ScreenName.GameInfoTextScreen);

            GameManager.inst.EnableBat();
            GameManager.inst.SetDataForNextPlayer();
        }
    }
}






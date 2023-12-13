using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace Yudiz.VRBowling
{
    public class GameManager : MonoBehaviour
    {
        public int tempRoundCount;
        public int tempTurnCount;
        public int finalTurnCount;
        public int finalroundCount;
        public Transform ballSpawn;
        public Transform pinSpawn;

        public GameObject ballPrefab;
        public GameObject pinPrefab;
        public GameObject CurrentPinObj;

        private float ballDistance = 0.5f;
        public int pinCount { get; set; }
        public static GameManager instance;
        public List<ArrowLight> arrowLighting;
        public XRDirectInteractor leftHand;
        public XRDirectInteractor RightHand;
        public List<Vector3> PinPos;
        public const string rightHandName = "Right Direct Interactor";

        public static event Action OnGameOverScore;
        public List<Material> BallMat;
        public List<GameObject> Ball;
        public bool TouchWalls;
        //public ArrowLight arrowLight;
        //public int oneTurnScore;
        //public int twoTurnScore;

        public IEnumerator ArrowLightAnimation()
        {
            for (int i = 0; i < arrowLighting.Count; i++)
            {

                arrowLighting[i].LightOn();
                yield return new WaitForSeconds(0.05f);

            }
            StartCoroutine("ArrowLightAnimation");
        }

        public List<Pin> hitPin;
        public int hitPinCount;


        public void Awake()
        {
            instance = this;
        }
        public async void PlayGame()
        {
            SoundManager.inst.SoundPlay(SoundManager.SoundName.Click);
            for (int i = 0; i < UIManager.instance._ScoreSytemCanvas.roundImage.Count; i++)
            {
                UIManager.instance._ScoreSytemCanvas.roundImage[i].enabled = false;
            }
            UIManager.instance.menuScreen.mainManuScreen.enabled = false;
            await Task.Delay(2000);
            tempTurnCount = finalTurnCount;
            tempRoundCount = 1;
            SoundManager.inst.CurrentRounds(SoundManager.RoundSound.Round_1);
            PinInstanciat();
            StopCoroutine("ArrowLightAnimation");
            BallInstanciat();
            UIManager.instance._ScoreSytemCanvas.roundImage[tempRoundCount - 1].enabled = true;
        }
        // Start is called before the first frame update
        void Start()
        {

            StartCoroutine("ArrowLightAnimation");


        }

        public void BallInstanciat()
        {
            for (int i = 0; i < finalTurnCount; i++)
            {
                GameObject obj = Instantiate(ballPrefab, new Vector3(ballSpawn.position.x, ballSpawn.position.y, ballSpawn.position.z + (i * ballDistance)), ballSpawn.rotation);
                obj.GetComponent<MeshRenderer>().material = BallMat[i];
                Ball.Add(obj);

            }
        }
        public void PinInstanciat()
        {

            CurrentPinObj = Instantiate(pinPrefab, pinSpawn.transform.position, pinSpawn.transform.rotation);

        }

        public void RoundChange()
        {
            //ScoreManager.instance.RoundwiseScore(tempRoundCount);
            if (finalroundCount != tempRoundCount)
            {
                Debug.Log("-------RoundChange----");
                tempTurnCount = finalTurnCount;
                Destroy(CurrentPinObj);
                PinInstanciat();
                BallInstanciat();
                tempRoundCount++;
                for (int i = 0; i < UIManager.instance._ScoreSytemCanvas.roundImage.Count; i++)
                {
                    UIManager.instance._ScoreSytemCanvas.roundImage[i].enabled = false;
                }
                switch (tempRoundCount)
                {
                    case 1:
                        SoundManager.inst.CurrentRounds(SoundManager.RoundSound.Round_1);
                        hitPinCount = 0;
                        break;
                    case 2:
                        SoundManager.inst.CurrentRounds(SoundManager.RoundSound.Round_2);
                        hitPinCount = 0;
                        break;
                    case 3:
                        SoundManager.inst.CurrentRounds(SoundManager.RoundSound.Round_3);
                        hitPinCount = 0;
                        break;
                    case 4:
                        SoundManager.inst.CurrentRounds(SoundManager.RoundSound.Round_4);
                        hitPinCount = 0;
                        break;
                    case 5:
                        SoundManager.inst.CurrentRounds(SoundManager.RoundSound.Round_5);
                        hitPinCount = 0;
                        break;
                    default:
                        break;
                }
                UIManager.instance._ScoreSytemCanvas.roundImage[tempRoundCount - 1].enabled = true;


            }
            else
            {

                UIManager.instance._gameOverCanvas.gameOverCanvas.enabled = true;

                OnGameOverScore();

            }

        }
        public void TurnChange()
        {

            Debug.Log("-----ChangeTurn------");
            tempTurnCount--;
            ScoreManager.instance.RoundwiseScore(tempRoundCount);
            if (tempTurnCount > 0)
            {
                if (hitPinCount == 10)
                {
                    for (int i = 0; i < Ball.Count; i++)
                    {
                        if (Ball.Count != null)
                        {
                            Destroy(Ball[i].gameObject);
                        }
                    }
                    RoundChange();
                }
                else
                {
                    SoundManager.inst.SoundPlay(SoundManager.SoundName.Trunchange);
                }

            }
            else if (tempTurnCount == 0)
            {
                RoundChange();
            }
            EnableAllInteractor();

        }

        public async void RemovePin()
        {
            Debug.Log("------RemovePin---");
            await Task.Delay(1800);
            hitPinCount = hitPin.Count;
            for (int i = 0; i < hitPin.Count; i++)
            {
                Destroy(hitPin[i].gameObject);
            }
            hitPin.Clear();
            await Task.Delay(100);
            for (int i = 0; i < CurrentPinObj.transform.childCount; i++)
            {
                CurrentPinObj.transform.GetChild(i).GetComponent<Pin>().Reset();
            }
            TurnChange();
            for (int i = 0; i < arrowLighting.Count; i++)
            {
                arrowLighting[i].LightOff();
            }
            EnableAllInteractor();
        }
        public void DisableOtherGrabInteractor(string handName)
        {
            if (handName == rightHandName)
                leftHand.enabled = false;
            else
                RightHand.enabled = true;
        }

        public void EnableAllInteractor()
        {
            leftHand.enabled = true;
            RightHand.enabled = true;
        }

        public void PlayAgin()
        {
            UIManager.instance._gameOverCanvas.gameOverCanvas.enabled = false;
            ScoreManager.instance.ResetScore();
            Destroy(CurrentPinObj);
            EnableAllInteractor();
            for (int i = 0; i < arrowLighting.Count; i++)
            {
                arrowLighting[i].LightOff();
            }
            Debug.Log("------RemovePin---");
            hitPinCount = hitPin.Count;
            for (int i = 0; i < hitPin.Count; i++)
            {
                Destroy(hitPin[i].gameObject);
            }
            hitPin.Clear();
            for (int i = 0; i < Ball.Count; i++)
            {
                Destroy(Ball[i].gameObject);
            }
            Ball.Clear();
            PlayGame();
        }
        public void Home()
        {
            UIManager.instance._gameOverCanvas.gameOverCanvas.enabled = false;


            ScoreManager.instance.ResetScore();
            Destroy(CurrentPinObj);
            EnableAllInteractor();
            for (int i = 0; i < arrowLighting.Count; i++)
            {
                arrowLighting[i].LightOff();
            }
            Debug.Log("------RemovePin---");
            hitPinCount = hitPin.Count;
            for (int i = 0; i < hitPin.Count; i++)
            {
                Destroy(hitPin[i].gameObject);
            }
            hitPin.Clear();
            for (int i = 0; i < Ball.Count; i++)
            {
                Destroy(Ball[i].gameObject);
            }
            Ball.Clear();
            StartCoroutine("ArrowLightAnimation");
            UIManager.instance.menuScreen.mainManuScreen.enabled = true;
        }


    }
}

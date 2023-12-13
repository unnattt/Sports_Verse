using Agora_RTC_Plugin.API_Example;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
namespace Yudiz.VRBowling
{
    public class Ball : MonoBehaviour
    {
        public XRGrabInteractable grabInteractable;
        public Transform pickLeft;
        public Transform pickRight;
        public Rigidbody rb;
        public IEnumerator test;

        //public float startSpeed = 40f; // the speed at which the ball starts moving

        //private Transform _arrow;

        //private bool _ballMoving;

        //private Transform _startPosition;

        //private List<GameObject> _pins = new();

        //private readonly Dictionary<GameObject, Transform> _pinsDefaultTransform = new();

        //public int Point { get; set; }

        ////[SerializeField] private Animator cameraAnim;

        ////private TextMeshProUGUI feedBack;





        //private void Start()
        //{
        //    Application.targetFrameRate = 60;

        //    _arrow = GameObject.FindGameObjectWithTag("Arrow").transform;

        //    // get the reference to the Rigidbody component of the ball
        //    rb = GetComponent<Rigidbody>();

        //    _startPosition = transform;

        //    _pins = GameObject.FindGameObjectsWithTag("Pin").ToList();

        //    foreach (var pin in _pins)
        //    {
        //        _pinsDefaultTransform.Add(pin, pin.transform);
        //    }

        //    //        feedBack = GameObject.FindGameObjectWithTag("FeedBack").GetComponent<TextMeshProUGUI>();
        //}

        //void Update()
        //{
        //    //if (_ballMoving)
        //    //{
        //    //    return;
        //    //}

        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        StopAllCoroutines();
        //        test = Shoot();
        //        StartCoroutine(test);
        //    }

        //}

        //private IEnumerator Shoot()
        //{
        //    //cameraAnim.SetTrigger("Go");
        //    //cameraAnim.SetFloat("CameraSpeed", _arrow.transform.localScale.z);
        //    // _ballMoving = true;
        //    //_arrow.gameObject.SetActive(false);
        //    rb.isKinematic = false;

        //    // calculate the force vector to apply to the ball
        //    Vector3 forceVector = _arrow.forward * (startSpeed * _arrow.transform.localScale.x);

        //    // calculate the position at which to apply the force (in this case, the center of the ball)
        //    Vector3 forcePosition = transform.position + (transform.right * 0.5f);

        //    // apply the force at the specified position
        //    rb.AddForceAtPosition(forceVector, forcePosition, ForceMode.Impulse);


        //    //yield return new WaitForSeconds(7);

        //    // _ballMoving = false;

        //    // GenerateFeedBack();

        //    yield return new WaitForSeconds(10);
        //    Destroy(this.gameObject);

        //    ResetGame();
        //}


        //private void ResetGame()
        //{
        //    StopAllCoroutines();
        //    GameManager.instance.RemovePin();

        //}


        //private void GenerateFeedBack()
        //{
        //    feedBack.text = Point switch
        //    {
        //        0 => "Nothing!",
        //        > 0 and < 3 => "You are learning Now!",
        //        >= 3 and < 6 => "It was close!",
        //        >= 6 and < 10 => "It was nice!",
        //        _ => "Perfect! You are a master!"
        //    };

        //    feedBack.GetComponent<Animator>().SetTrigger("Show");
        //}




        /// <summary>
        ///  FOR -------------------VR-------------------------
        /// </summary>

        private void Start()
        {
            grabInteractable.hoverEntered.AddListener(Hover);
            grabInteractable.selectEntered.AddListener(getCurrentBall);
            grabInteractable.selectExited.AddListener(ReleaseBall);

            //Application.targetFrameRate = 60;

        }
        private void Hover(HoverEnterEventArgs arg0)
        {
            Debug.Log("----Hover----");
            if (arg0.interactorObject.transform.name.Contains("Right"))
            {

                grabInteractable.attachTransform = pickRight;


            }
            else
            {
                grabInteractable.attachTransform = pickLeft;
            }
        }
        private void getCurrentBall(SelectEnterEventArgs arg0)
        {
            Debug.Log("--------CurrentBall---------");
            GameManager.instance.DisableOtherGrabInteractor(arg0.interactorObject.transform.gameObject.name);
        }

        public void ReleaseBall(SelectExitEventArgs arg0)
        {
            Debug.Log("------Release-------");

            test = Shoot();
            StartCoroutine(test);
            GameManager.instance.leftHand.enabled = false;
            GameManager.instance.RightHand.enabled = false;


        }
        private IEnumerator Shoot()
        {
            Debug.Log("------Shoot-------");
            //SoundManager.inst.VoiceSoundPlay(SoundManager.VoiceSound.PleaseWait);
            yield return new WaitForSeconds(9);
            Debug.Log("----Stop---");
            ResetGame();
        }
        private void ResetGame()
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
            GameManager.instance.Ball.Remove(this.gameObject);
            GameManager.instance.RemovePin();
        }



    }
}

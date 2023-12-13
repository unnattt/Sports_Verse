using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
namespace Yudiz.VRBowling
{
    public class Pin : MonoBehaviour
    {
        private bool CollitionCheck = false;
        public Vector3 pinPos;
        public Quaternion pinRo;

        public async void Start()
        {
            await Task.Delay(1000);
            pinPos = transform.position;
            pinRo = transform.rotation;

        }
        public async void OnTriggerEnter(Collider others)
        {
            if (CollitionCheck == false)
            {
                if (others.gameObject.tag == "Surface" || others.gameObject.tag == "PinTop" || others.gameObject.tag == "Ball" || others.gameObject.tag == "Pin")
                {
                    CollitionCheck = true;
                    GameManager.instance.hitPin.Add(this.gameObject.GetComponentInChildren<Pin>());
                    //this.gameObject.GetComponent<Pin>().enabled = false;
                    await Task.Delay(500);
                }
            }
        }
        public void Reset()
        {
            transform.position = pinPos;
            transform.rotation = pinRo;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


        }




    }
}

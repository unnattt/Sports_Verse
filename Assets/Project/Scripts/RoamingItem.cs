using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sportsverse.World
{
    public class RoamingItem : MonoBehaviour
    {
        public float Speed;
        public CinemachineDollyCart cart;

        void Start()
        {
            cart = GetComponentInChildren<CinemachineDollyCart>();
        }

        // Update is called once per frame
        void Update()
        {
            cart.m_Position += Time.deltaTime * Speed;
        }
    }
}
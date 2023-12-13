
using Sportsverse.World;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCDogAnimationController : MonoBehaviour
{
    NPCPet npcPet;
    Animator dogAnim;

    private void Awake()
    {
        npcPet = GetComponent<NPCPet>();
        dogAnim = GetComponent<Animator>();
    }

    Vector3 previousPosition; float curSpeed;
    void Update()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        dogAnim.SetTrigger("Blink_tr");
        dogAnim.SetFloat("Movement_f", Mathf.Clamp01(curSpeed));
    }
    
}
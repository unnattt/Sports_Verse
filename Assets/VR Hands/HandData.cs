using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public HandSide handType;
    public Transform root;
    public Animator animator;
    public GameObject prefab;
    public Transform[] fingerBones;
}

public enum HandSide
{
    Right,
    Left
}
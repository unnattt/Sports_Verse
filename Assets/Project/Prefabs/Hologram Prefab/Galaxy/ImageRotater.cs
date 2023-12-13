using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotater : MonoBehaviour
{
    public float rotationSpeed = 45f; // Adjust the rotation speed as need

    
    void Update()
    {
       
          // Rotate the GameObject around the y-axis smoothly
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);       
    }
}

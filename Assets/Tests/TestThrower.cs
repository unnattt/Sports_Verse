using Sportsverse.World;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestThrower : MonoBehaviour
{
    public Bone bone;

    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.T))
        {
            bone.Throw(GetForce(500), transform.position);
        }*/
    }



    private Vector3 GetForce(float magnitude)
    {
        Vector3 randomDirection = Random.onUnitSphere;

        // Decompose this direction into horizontal and vertical components.
        Vector3 horizontalDirection = new Vector3(randomDirection.x, 0, randomDirection.z).normalized;
        Vector3 verticalDirection = Vector3.up;

        // Calculate the final force based on a 45-degree projectile angle.
        return magnitude * (horizontalDirection + verticalDirection).normalized;

    }
}

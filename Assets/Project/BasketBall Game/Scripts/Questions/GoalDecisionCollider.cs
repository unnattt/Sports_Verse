using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalDecisionCollider : MonoBehaviour
{
    private Yudiz.VRBasketBall.Core.Ball ball;
    public static bool optionAnswer;
    public Transform netCenterPoint;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ans"+"---"+optionAnswer);
            ball = other.gameObject.GetComponent<Yudiz.VRBasketBall.Core.Ball>();
            
            if (optionAnswer)
            {
                // ball.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                // ball.transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                ball.transform.DOMove(netCenterPoint.position, 0.2f);
            }
            else
            {
                //ball.transform.DOMove(netCenterPoint.position + new Vector3(1, 1,1), 0.2f);
                Vector3 posibility1 = new Vector3(Random.Range(2,4), Random.Range(2, 4), Random.Range(2, 4));
                Vector3 posibility2 = new Vector3(Random.Range(-4, -2), Random.Range(-4, -2), Random.Range(-4, -2));

                int randomIndex = Random.Range(0, 2);

                Vector3 selectedVector = (randomIndex == 0) ? posibility1 : posibility2;
                ball.gameObject.GetComponent<Rigidbody>().AddForce(netCenterPoint.position + selectedVector, ForceMode.Impulse);
            }

        }
    }
}

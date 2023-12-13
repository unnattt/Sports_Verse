using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollisionDetectQuestionsUI : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {
            UISystem.ViewController.Instance.ShowPopup(UISystem.PopupName.QuestionsScreen);
            Time.timeScale = 0;
           
        }
        
    }
}

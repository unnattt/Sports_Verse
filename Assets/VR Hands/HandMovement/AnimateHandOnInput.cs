using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [Header("Input Action Property")]
    [SerializeField] private InputActionProperty _pinchInput;
    [SerializeField] private InputActionProperty _gripInput;

    [Header("Animation")]
    [SerializeField] private Animator _handAnimator;
    private float _triggerValue;
    private float _gripValue;

    private void Update()
    {
        //Pinch
        _triggerValue = _pinchInput.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);

        //Grip
        _gripValue = _gripInput.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }

}

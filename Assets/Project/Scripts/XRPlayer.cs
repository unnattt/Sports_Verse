using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yudiz.BaseFramework;

public class XRPlayer : Yudiz.BaseFramework.Singleton<XRPlayer>
{
    [SerializeField] private InputActionReference rightJoystickInputAction;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform avatarParent;

    private Transform xrCameraTransform;

    private float unAlignedTime;
    private float playerAvatarAlignmentTime = 3;
    private float thresholdAvatarMoveAngle = 10;
    private float alignmentSpeed = 1;

    public override void OnAwake()
    {
        base.OnAwake();
        xrCameraTransform = Camera.main.transform;
    }
    //private void OnEnable()
    //{
    //    rightJoystickInputAction.action.started += OnJoystickedMoved;
    //    rightJoystickInputAction.action.performed += OnJoystickedMoved;
    //    rightJoystickInputAction.action.canceled += OnJoystickedMoved;
    //}
    //private void OnDisable()
    //{
    //    rightJoystickInputAction.action.started -= OnJoystickedMoved;
    //    rightJoystickInputAction.action.performed -= OnJoystickedMoved;
    //    rightJoystickInputAction.action.canceled -= OnJoystickedMoved;
    //}
    public void TeleportPlayer(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void SetObjectInPlayerCameraForward(Transform objectToSet, float distanceFromPlayer = 0.5f)
    {
        Vector3 cameraForward = xrCameraTransform.forward;
        cameraForward.y = 0;
        Vector3 newPosition = xrCameraTransform.position + cameraForward * distanceFromPlayer;
        objectToSet.SetPositionAndRotation(newPosition, Quaternion.LookRotation(cameraForward));
    }


    //private void OnJoystickedMoved(InputAction.CallbackContext obj)
    //{
    //    Vector2 movementInput = obj.ReadValue<Vector2>();
    //    characterAnimator.SetFloat("Movement", movementInput.y);
    //}

    //private void Update()
    //{
    //    if (Mathf.Abs(Vector3.SignedAngle(avatarParent.forward, xrCameraTransform.forward, Vector3.up)) > thresholdAvatarMoveAngle)
    //    {
    //        unAlignedTime += Time.deltaTime;
    //    }
    //    else
    //    {
    //        unAlignedTime = 0;
    //    }
    //    if(unAlignedTime > playerAvatarAlignmentTime)
    //    {
    //        Vector3 lookDir = xrCameraTransform.forward;
    //        lookDir.y = 0;
    //        avatarParent.transform.rotation = Quaternion.Lerp(avatarParent.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), Time.deltaTime * alignmentSpeed);
    //    }
    //}
}

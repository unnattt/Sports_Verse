using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Yudiz.BaseFramework;

public class XRNetworkPlayer : MonoBehaviour
{
    [SerializeField] private InputActionReference rightJoystickInputAction;
    [SerializeField] private InputActionReference menuInputAction;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform avatarParent;
    [SerializeField] private string avatarLayerName;
    [SerializeField] private List<SkinnedMeshRenderer> playerAvatars = new List<SkinnedMeshRenderer>();

    private Transform xrCameraTransform;

    private float unAlignedTime;
    private float playerAvatarAlignmentTime = 3;
    private float thresholdAvatarMoveAngle = 10;
    private float alignmentSpeed = 1;


    #region NETWORK_CHANING_CONTENT
    [SerializeField] private List<GameObject> controllers;

    [SerializeField] private Camera playerCamera;
    private TrackedPoseDriver playerTrackedPoseDriver;
    private LocomotionSystem locomotionSystem;
    private ActionBasedContinuousMoveProvider continuousMoveProvider;
    private ActionBasedSnapTurnProvider snapTurnProvider;
    private XROrigin playerXROrigin;
    private AudioListener playerAudioListener;
    private XRCustomTeleportation xrCustomTeleportation;

    private int avatarIndex;
    private bool isNavPanelActive = false;
    private bool AllowPlayerControl = true;
    #endregion

    private void Start()
    {

        playerCamera = GetComponentInChildren<Camera>();
        xrCameraTransform = playerCamera.transform;

        playerXROrigin = GetComponent<XROrigin>();
        locomotionSystem = GetComponent<LocomotionSystem>();
        continuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        snapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
        xrCustomTeleportation = GetComponent<XRCustomTeleportation>();

        //Camera Components
        playerTrackedPoseDriver = playerCamera.GetComponent<TrackedPoseDriver>();
        playerAudioListener = playerCamera.GetComponent<AudioListener>();



        avatarParent.gameObject.SetLayerRecursively(LayerMask.NameToLayer("PlayerAvatar"));
        Sportsverse.GameManager.Instance.AssignLocalPlayer(this);

        avatarIndex = Random.Range(0, playerAvatars.Count);
        SetRandomPlayer(avatarIndex);

    }
    public void OnEnable()
    {

        rightJoystickInputAction.action.started += OnJoystickedMoved;
        rightJoystickInputAction.action.performed += OnJoystickedMoved;
        rightJoystickInputAction.action.canceled += OnJoystickedMoved;

        //menuInputAction.action.performed += OnMenuPressed;

    }
    public void OnDisable()
    {
        rightJoystickInputAction.action.started -= OnJoystickedMoved;
        rightJoystickInputAction.action.performed -= OnJoystickedMoved;
        rightJoystickInputAction.action.canceled -= OnJoystickedMoved;

        //menuInputAction.action.performed -= OnMenuPressed;
    }

    private void OnMenuPressed(InputAction.CallbackContext obj)
    {
        if (isNavPanelActive)
        {
            PopUpController.Instance.HidePopup(PopUpType.NavigationBarPopup);
        }
        else
        {
            PopUpController.Instance.ShowPopup(PopUpType.NavigationBarPopup);
        }
        isNavPanelActive = !isNavPanelActive;
    }

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

    public void TogglePlayerControl(bool enable)
    {
        AllowPlayerControl = enable;
        xrCustomTeleportation.enabled = enable;
        continuousMoveProvider.enabled = enable;
        snapTurnProvider.enabled = enable;
        locomotionSystem.enabled = enable;
    }


    private void OnJoystickedMoved(InputAction.CallbackContext obj)
    {
        Vector2 movementInput = obj.ReadValue<Vector2>();
        characterAnimator.SetFloat("VelocityX", movementInput.x);
        characterAnimator.SetFloat("VelocityZ", movementInput.y);
    }

    private void Update()
    {
        if (Mathf.Abs(Vector3.SignedAngle(avatarParent.forward, xrCameraTransform.forward, Vector3.up)) > thresholdAvatarMoveAngle)
        {
            unAlignedTime += Time.deltaTime;
        }
        else
        {
            unAlignedTime = 0;
        }
        if (unAlignedTime > playerAvatarAlignmentTime)
        {
            Vector3 lookDir = xrCameraTransform.forward;
            lookDir.y = 0;
            avatarParent.transform.rotation = Quaternion.Lerp(avatarParent.transform.rotation, Quaternion.LookRotation(lookDir, Vector3.up), Time.deltaTime * alignmentSpeed);
        }
    }

    private void RemoveAndDisableOtherPlayerComponent()
    {

        playerCamera.enabled = false;
        playerAudioListener.enabled = false;
        Destroy(playerXROrigin);
        //Destroy(playerTrackedPoseDriver);
        playerTrackedPoseDriver.enabled = false;
        Destroy(continuousMoveProvider);
        Destroy(snapTurnProvider);
        Destroy(locomotionSystem);

        foreach (GameObject controller in controllers)
        {
            controller.SetActive(false);
        }
    }
    private void SetRandomPlayer(int index)
    {
        Debug.Log("Avatar Index " + index);
        foreach (SkinnedMeshRenderer avatar in playerAvatars)
        {
            avatar.enabled = false;
        }
        playerAvatars[index].enabled = true;
    }
}

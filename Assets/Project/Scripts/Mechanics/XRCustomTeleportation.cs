using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCustomTeleportation : MonoBehaviour
{
    public XRPlayer player;
    public XRNetworkPlayer networkPlayer;

    public XRRayInteractor teleportRayInteractor;
    public InteractorSwitcher interactorSwitcher;

    public InputActionReference teleportActivationInput;

    private bool canTeleport = false;

    private void OnEnable()
    {
        teleportActivationInput.action.performed += OnTeleportationAttempted;
        teleportActivationInput.action.canceled += OnTeleportationAttempted;
    }


    private void OnDisable()
    {
        teleportActivationInput.action.performed -= OnTeleportationAttempted;
        teleportActivationInput.action.canceled -= OnTeleportationAttempted;
    }


    private void OnTeleportationAttempted(InputAction.CallbackContext obj)
    {
        Vector2 axis = obj.ReadValue<Vector2>();
        //Debug.Log("Teleport " + axis.y);
        if (axis.y > 0.9f)
        {
            canTeleport = true;
            interactorSwitcher.ToggleInteraction(true);
        }
        else
        {
            if (canTeleport)
            {
                TeleportIfPossible();
            }
            canTeleport = false;
            interactorSwitcher.ToggleInteraction(false);
        }
    }
    public void TeleportIfPossible()
    {
        if (teleportRayInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget))
        {
            if (isValidTarget)
            {
                if (player != null)
                {
                    player.TeleportPlayer(position, player.transform.rotation);
                }
                else if(networkPlayer!= null)
                {
                    networkPlayer.TeleportPlayer(position, networkPlayer.transform.rotation);
                }
            }
            else
            {
                Debug.Log("Trying to Teleporting on Invalid Target");
            }
        }
    }
}

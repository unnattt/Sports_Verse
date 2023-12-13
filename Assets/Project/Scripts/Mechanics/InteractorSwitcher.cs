using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class InteractorSwitcher : MonoBehaviour
{
    public struct InteractorController
    {
        public GameObject m_GO;
        public XRController m_XRController;
        public XRInteractorLineVisual m_LineRenderer;
        public XRBaseInteractor m_Interactor;

        public void Attach(GameObject gameObject)
        {
            m_GO = gameObject;
            if (m_GO != null)
            {
                m_XRController = m_GO.GetComponent<XRController>();
                m_LineRenderer = m_GO.GetComponent<XRInteractorLineVisual>();
                m_Interactor = m_GO.GetComponent<XRBaseInteractor>();
            }
        }

        public void Activate()
        {
            if (m_LineRenderer)
            {
                m_LineRenderer.enabled = true;
            }
            if (m_XRController)
            {
                m_XRController.enableInputActions = true;
            }
            if (m_Interactor)
            {
                m_Interactor.enabled = true;
            }
        }

        public void Deactivate()
        {
            if (m_LineRenderer)
            {
                m_LineRenderer.enabled = false;
            }
            if (m_XRController)
            {
                m_XRController.enableInputActions = false;
            }
            if (m_Interactor)
            {
                m_Interactor.enabled = false;
            }
        }
    }

    [SerializeField] private GameObject normalInteractor;
    [SerializeField] private GameObject activableInteractor;

    InteractorController normalInteraction;
    InteractorController activableInteraction;

    bool isActivated;


    private void Start()
    {
        normalInteraction.Attach(normalInteractor);
        activableInteraction.Attach(activableInteractor);

        normalInteraction.Activate();
        activableInteraction.Deactivate();
    }

    public void ToggleInteraction(bool activate)
    {
        if (activate == isActivated)
            return;
        if(activate)
        {
            normalInteraction.Deactivate();
            activableInteraction.Activate();
        }
        else
        {
            normalInteraction.Activate();
            activableInteraction.Deactivate();
        }
        isActivated = activate;
    }
}

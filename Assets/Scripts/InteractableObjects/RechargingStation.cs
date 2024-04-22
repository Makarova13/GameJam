using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RechargingStation : MonoBehaviour
{
    [SerializeField] private float rechargingDuration = 1f;
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject rechargingBar;
    [SerializeField] private RectTransform starPos;
    [SerializeField] private RectTransform endPos;
    [SerializeField] private RectTransform positionMarker;

    private Player player;
    private InputActions inputActions;

    private float interactionTimer = 0;
    private bool isInteracting = false;


    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.ChargeStation.Interact.performed += OnStartInteraction;
        inputActions.ChargeStation.Interact.canceled += OnStopInteraction;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }

        if(isInteracting)
        {
            interactionTimer += Time.deltaTime;
            UpdateMarkerPosition();
        }

        if (interactionTimer >= rechargingDuration)
        {
            player.GetFlashLight().Recharge();
            ResetStation();
        }
    }

    private void UpdateMarkerPosition()
    {
        positionMarker.position = starPos.position
            + (interactionTimer / rechargingDuration) * (endPos.position - starPos.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            this.player = player;
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(player == null)
        {
            return;
        }

        if(player.gameObject == other.gameObject)
        {
            player = null;
            interactionText.SetActive(false);
        }
    }

    private void OnStartInteraction(InputAction.CallbackContext context)
    {
        if (player == null)
        {
            return;
        }

        isInteracting = true;
        player.GetFlashLight().SetIsOn(false);
        interactionText.SetActive(false);
        rechargingBar.SetActive(true);
    }

    private void OnStopInteraction(InputAction.CallbackContext context)
    {
        ResetStation();
    }


    private void ResetStation()
    {
        player = null;
        isInteracting = false;

        interactionTimer = 0;

        rechargingBar.SetActive(false);
    }

    public bool GetIsInteracting()
    {
        return isInteracting;
    }
}

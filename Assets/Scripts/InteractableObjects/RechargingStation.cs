using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RechargingStation : MonoBehaviour
{
    [SerializeField] private float rechargingDuration = 1f;

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
        }

        if (interactionTimer >= rechargingDuration)
        {
            Debug.Log("Succedded");
            isInteracting = false;
            interactionTimer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            this.player = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(player.gameObject == other.gameObject)
        {
            player = null;
        }
    }

    private void OnStartInteraction(InputAction.CallbackContext context)
    {
        isInteracting = true;


    }

    private void OnStopInteraction(InputAction.CallbackContext context)
    {
        isInteracting = false;

        interactionTimer = 0;
    }

}

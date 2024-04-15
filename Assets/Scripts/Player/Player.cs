using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    [Header("Components")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Health health;
    [Space]
    [Header("Vectors")]
    private Vector3 movementInput;
    [Space]
    [Header("Floats")]
    private float PlayerSpeed;


    private InputActions inputActions;

    public Health GetHealth() => health;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;

        inputActions = new InputActions();
        inputActions.PlayerInput.Test.performed += ctx => OnTestPerformed();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    private void OnTestPerformed()
    {
        health.Damage(1);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        Speed = 60f;
    }

    void FixedUpdate()
    {
        movementInput = movement.action.ReadValue<Vector3>();
        playerRB.AddForce(movementInput * Speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public float Speed
    {
        get { return PlayerSpeed;  }
        set
        {
            if(value < 100)
            {
                PlayerSpeed = value;
            }
        }
    } 
}

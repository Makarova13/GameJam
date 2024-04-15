using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private Rigidbody playerRB;
    [Space]
    [Header("Vectors")]
    private Vector3 movementInput;
    [Space]
    [Header("Floats")]
    private float PlayerSpeed;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody playerRB;
    [Space]
    [Header("Vectors")]
    private Vector3 movementInput;
    [Space]
    [Header("Floats")]
    private float PlayerSpeed;
    private float CurrentSpeed;
    private float Direction = 0;

    private void Start()
    {
        Speed = 60f;
    }

    void FixedUpdate()
    {
        movementInput = movement.action.ReadValue<Vector3>();
        CurrentSpeed = movementInput.magnitude * PlayerSpeed;
        playerRB.AddForce(movementInput * Speed * Time.fixedDeltaTime, ForceMode.Impulse);
        animator.SetFloat("Walking", CurrentSpeed);
        if (movementInput.x < 0)
        {
            Debug.Log("Links");
        } else if(movementInput.x > 0)
        {
            Debug.Log("Rechts");
        } else if(movementInput.z < 0)
        {
            Debug.Log("Naar Beneden");
        } else if(movementInput.z > 0)
        {
            Debug.Log("Naar Boven");
        }
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

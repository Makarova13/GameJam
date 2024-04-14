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
    private float PlayerSpeed = 10f;

    void FixedUpdate()
    {
        movementInput = movement.action.ReadValue<Vector3>();
        playerRB.AddForce(movementInput * PlayerSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }
}

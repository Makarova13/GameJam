using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Components")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Health health;
    [SerializeField] private Transform testEnemy;
    [Space]
    [Header("Vectors")]
    private Vector3 movementInput;
    [Space]
    [Header("Floats")]
    private float PlayerSpeed;
    [Header("Bools")]
    private bool isAttacking = false;


    private InputActions inputActions;

    public Health GetHealth() => health;

    private void Awake()
    {
        if (instance != null)
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

        health.OnDeath += Health_OnDeath;
    }

    private void Health_OnDeath()
    {
        UIManager.Instance.OpenDeathPopup();
    }

    void FixedUpdate()
    {
        // Movement
        movementInput = movement.action.ReadValue<Vector3>();
        playerRB.AddForce(movementInput * Speed * Time.fixedDeltaTime, ForceMode.Impulse);
        if (movementInput != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        inputActions.PlayerInput.Attack.performed += ctx => Attack();
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", isAttacking);
            StartCoroutine(AttackRoutine());
            Debug.Log("Attack Success");
            if (Vector3.Distance(this.transform.position, testEnemy.position) < 3f)
            {
                // hit
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(2);
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        if (input != Vector3.zero)
        {
            animator.SetFloat("X-Input", input.x);
            animator.SetFloat("Z-Input", input.z);
        }

        if (input.x > 0) // Right
        {

        }
        else if (input.x < 0) // Left
        {

        }
        else if (input.z > 0) // Top
        {

        }
        else if (input.z < 0) // Down
        {

        }
    }

    public float Speed
    {
        get { return PlayerSpeed; }
        set
        {
            if (value < 100)
            {
                PlayerSpeed = value;
            }
        }
    }
}


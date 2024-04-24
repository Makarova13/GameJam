using Assets.Scripts;
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
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Health health;
    [SerializeField] private FlashLightController flashLightController;
    [SerializeField] private WeaponController weaponController;
    [Space]
    [Header("Vectors")]
    private Vector3 movementInput;
    [Space]
    [Header("Floats")]
    private float PlayerSpeed;
    private float Range;
    [Header("Bools")]
    private bool isAttacking = false;
    private bool hasWeapon;
    [Header("Ints")]
    private int Damage;


    private InputActions inputActions;

    public Health GetHealth() => health;
    public FlashLightController GetFlashLight() => flashLightController;
    public WeaponController GetWeaponController() => weaponController;

    public List<Transform> TargetsForEnemy = new List<Transform>();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        inputActions = new InputActions();
        inputActions.PlayerInput.Test.performed += ctx => OnTestPerformed();
        inputActions.PlayerInput.FlashLight.performed += ctx => OnFlashLightToggle();
        movement.action.performed += Movement;

            inputActions.PlayerInput.Attack.performed += ctx => Attack();
            inputActions.PlayerInput.Dash.performed += ctx => Dash.instance.Player_Dash();

        TargetsForEnemy.Add(transform);
    }

        private void OnFlashLightToggle()
        {
            Debug.Log("lantern loged");
            flashLightController.ToggleLight();
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

        SaveLoadManager.Instance.SavePosition(transform.position);
    }

    private void Health_OnDeath()
    {
        UIManager.Instance.OpenDeathPopup();
    }

    void FixedUpdate()
    {
        // Movement
        //movementInput = movement.action.ReadValue<Vector3>();
        playerRB.AddForce(movementInput.normalized * Speed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void Attack()
    {
        if (!isAttacking & !DialogSystemController.Instance.isActive)
        {
            Range = weaponController.CurrentData.Range;
            hasWeapon = weaponController.CurrentData.HasWeapon;
            Damage = weaponController.CurrentData.Damage;
            isAttacking = true;
            animator.SetBool("isAttacking", isAttacking);
            StartCoroutine(AttackRoutine());
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (GetDistance(enemy) < Range)
                {
                    if (hasWeapon)
                    {
                        enemy.gameObject.GetComponent<Health>().Damage(Damage);
                        Debug.Log(enemy.gameObject.GetComponent<Health>().GetCurrentHP());
                        Debug.Log(enemy);
                    }
                }
            }
        }
    }

    private float GetDistance(GameObject objects)
    {
        return Vector3.Distance(this.transform.position, objects.transform.position);
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

        movementInput = input;

        if (input.x > 0) // Right
        {
            flashLightController.Rotate(FlashLightController.Direction.Right);
        }
        else if (input.x < 0) // Left
        {
            flashLightController.Rotate(FlashLightController.Direction.Left);
        }
        else if (input.z > 0) // Top
        {
            flashLightController.Rotate(FlashLightController.Direction.Up);
        }
        else if (input.z < 0) // Down
        {
            flashLightController.Rotate(FlashLightController.Direction.Down);
        }

        if (movementInput != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("last-X-Input", movementInput.x);
            animator.SetFloat("last-Z-Input", movementInput.z);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void ResetPlayer(Vector3 pos)
    {
        transform.position = pos;
        health.ResetLife();
        flashLightController.Recharge();
    }

        public float Speed
        {
            get { return PlayerSpeed; }
            set
            {
                if (value < 150)
                {
                    PlayerSpeed = value;
                }
            }
        }
 }


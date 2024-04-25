using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private DeathPopup deathPopupPrefab;
    [SerializeField] private PauseMenu pauseMenuPrefab;

    private InputActions inputActions;
    private PauseMenu pauseMenu;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }

        Instance = this;

        inputActions = new InputActions();
        inputActions.UI.Escape.performed += OnEscapePresed;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnEscapePresed(InputAction.CallbackContext context)
    {
        if(pauseMenu != null)
        {
            return;
        }

        pauseMenu = Instantiate(pauseMenuPrefab, transform);
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    public void OpenDeathPopup()
    {
        Instantiate(deathPopupPrefab, transform);
    }
    
}

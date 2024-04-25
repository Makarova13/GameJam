using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button audioButton;
    [SerializeField] private Button tipsButton;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Tips tips;
    [SerializeField] private TextMeshProUGUI tipsText;


    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.UI.Escape.performed += OnEscapePressed;

        audioButton.onClick.AddListener(OnAudioButtonClicked);
        tipsButton.onClick.AddListener(OnTipsButtonClicked);
        mapButton.onClick.AddListener(OnMapButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        closeButton.onClick.AddListener(OnSoundButtonClicked);

        Time.timeScale = 0f;
    }

    private void OnSoundButtonClicked()
    {
        Close();
    }

    private void OnSettingsButtonClicked()
    {
        //settings
    }

    private void OnMapButtonClicked()
    {
        //map
    }

    private void OnTipsButtonClicked()
    {
        tipsText.text = tips.GetTip();
    }

    private void OnAudioButtonClicked()
    {
        //Change audio
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnEscapePressed(InputAction.CallbackContext context)
    {
        Close();
    }

    private void Close()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}

[Serializable]
public class Tips
{
    public List<string> tips;
    
    public string GetTip()
    {
        return tips[UnityEngine.Random.Range(0,tips.Count)];
    }
}

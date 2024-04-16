using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPopup : MonoBehaviour
{
    [SerializeField] private Button checkpointButton;
    [SerializeField] private Button restartLevel;
    [SerializeField] private Button quitGame;

    private void Awake()
    {
        checkpointButton.onClick.AddListener(OnCheckpointButtonClicked);
        restartLevel.onClick.AddListener(OnRestartButtonClicked);
        quitGame.onClick.AddListener(OnQuitButtonClicked);

        Time.timeScale = 0;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnRestartButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCheckpointButtonClicked()
    {
        //Todo
    }
}

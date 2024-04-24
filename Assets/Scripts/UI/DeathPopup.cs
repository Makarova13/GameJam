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

        Time.timeScale = 0.3f;
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void OnRestartButtonClicked()
    {
        Debug.Log("Restart");
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void OnCheckpointButtonClicked()
    {
        Debug.Log("CheckPoints");
        Time.timeScale = 1;
        Vector3 chackpointPos = SaveLoadManager.Instance.LoadPosition();
        Player.instance.ResetPlayer(chackpointPos);
        Destroy(gameObject);
    }
}

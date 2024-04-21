using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private Button quitGameButton;

    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartGameClicked);
        quitGameButton.onClick.AddListener(OnQuitGameClicked);
    }

    private void OnQuitGameClicked()
    {
        Application.Quit();
    }

    private void OnStartGameClicked()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
}

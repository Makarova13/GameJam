using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startGameButton, howToBtn, settingsBtn, creditsBtn, closeBtn_howTo, closeBtn_settings, closeBtn_credits;
    [SerializeField] private int gameSceneIndex = 1;
    [SerializeField] private GameObject howToPlay, mainScene, settings, credits;

    private void Awake()
    {
        startGameButton.onClick.AddListener(OnStartGameClicked);
        howToBtn.onClick.AddListener(OnHowTo);
        settingsBtn.onClick.AddListener(OnSettings);
        creditsBtn.onClick.AddListener(OnCredits);
    }

    private void OnHowTo()
    {
        mainScene.SetActive(false);
        howToPlay.SetActive(true);

        closeBtn_howTo.onClick.AddListener(Close);
    }

    private void OnSettings()
    {
        mainScene.SetActive(false);
        settings.SetActive(true);
        closeBtn_settings.onClick.AddListener(Close);
    }

    private void OnCredits()
    {
        mainScene.SetActive(false);
        credits.SetActive(true);
        closeBtn_credits.onClick.AddListener(Close);
    }

    private void Close()
    {
        if (howToPlay.activeSelf) howToPlay.SetActive(false);
        if (settings.activeSelf) settings.SetActive(false);
        if (credits.activeSelf) credits.SetActive(false);
        mainScene.SetActive(true);
    }

    private void OnStartGameClicked()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
}

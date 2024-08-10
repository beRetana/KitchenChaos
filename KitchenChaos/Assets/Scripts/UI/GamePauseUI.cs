using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour{

    [SerializeField] private Button mainMenuButton, resumeButton, optionsButton;
    [SerializeField] private OptionsUI optionsUI;

    private void Start(){
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        mainMenuButton.onClick.AddListener(() => {
            LoadingScene.LoadScene(LoadingScene.Scene.MainMenuScene);
        });
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() => {
            Hide();
            optionsUI.Show(Show);
        });
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    private void Show(){
        gameObject.SetActive(true);
        resumeButton.Select();
    }
}

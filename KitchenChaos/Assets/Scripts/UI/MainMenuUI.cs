using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button playButton, quitButton;

    private void Awake(){
        playButton.onClick.AddListener(() => {
            LoadingScene.LoadScene(LoadingScene.Scene.KitchenChaos);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}

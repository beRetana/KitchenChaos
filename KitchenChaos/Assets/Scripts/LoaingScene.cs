using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadingScene{

    public enum Scene{
        MainMenuScene,
        KitchenChaos,
        LoadingScene
    }

    private static Scene scene;

    public static void LoadScene(Scene scene){

        LoadingScene.scene = scene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadingSceneCallback(){
        SceneManager.LoadScene(scene.ToString());
    }
}

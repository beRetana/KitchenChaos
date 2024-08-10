using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneCallBack : MonoBehaviour
{

    private bool IsFirstFrame = true;

    void Update()
    {
        if(IsFirstFrame){
            IsFirstFrame = false;
            LoadingScene.LoadingSceneCallback();
        }
    }
}

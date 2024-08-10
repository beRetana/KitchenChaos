using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveWarning : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private Image warningImage;

    private void Start(){
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        warningImage.gameObject.SetActive(false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if(stoveCounter.IsFried() && e.progressNormalized >= 0.5f){
            warningImage.gameObject.SetActive(true);
        }
        else{
            warningImage.gameObject.SetActive(false);
        }
    }
}

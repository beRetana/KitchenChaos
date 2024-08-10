using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ProgressBarUI : MonoBehaviour {

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    [SerializeField] private IHasProgress iHasProgress;

    private void Start(){
        iHasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(iHasProgress == null){
            Debug.LogError("The GameObject: "+ hasProgressGameObject + "does not implement IHasProgress");
        }
        iHasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if( e.progressNormalized <= 0 || e.progressNormalized >= 1f){
            Hide();
        }else{
            Show();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}

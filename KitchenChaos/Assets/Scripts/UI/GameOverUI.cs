using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start(){
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsGameOver()){
            recipesDeliveredText.text = DeliveryManager.Instance.GetSucessfulRecipesDelivered().ToString();
            Show();
        }
        else{
            Hide();
        }
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}

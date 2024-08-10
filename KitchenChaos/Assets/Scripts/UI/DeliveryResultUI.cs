using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour{

    [SerializeField] private Image iconImage, background;
    [SerializeField] private TextMeshProUGUI deliveryResultText;
    [SerializeField] private Color sucessColor, failedColor;
    [SerializeField] private Sprite sucessSprite, failedSprite;

    private Animator animator;
    private const string POPUP = "PopUp";

    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        background.color = failedColor;
        iconImage.sprite = failedSprite;
        deliveryResultText.text = "Delivery\nFailed";
        animator.SetTrigger(POPUP);
    }

    private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        background.color = sucessColor;
        iconImage.sprite = sucessSprite;
        deliveryResultText.text = "Delivery\nSucess";
        animator.SetTrigger(POPUP);
    }
}

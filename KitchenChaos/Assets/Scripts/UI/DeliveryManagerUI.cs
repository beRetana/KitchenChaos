using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour{

    [SerializeField] private Transform container, recipeTemplate;

    private void Awake(){
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryCounter_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryCounter_OnRecipeCompleted;
    }

    private void DeliveryCounter_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisuals();
    }

    private void DeliveryCounter_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisuals();
    }

    public void UpdateVisuals(){
        foreach(Transform child in container){
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeOSList()){
            Transform recipeSOTransform = Instantiate(recipeTemplate, container);
            recipeSOTransform.gameObject.SetActive(true);
            recipeSOTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}

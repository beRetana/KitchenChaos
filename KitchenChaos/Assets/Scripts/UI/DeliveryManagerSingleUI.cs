using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer, iconTemplate;

    private void Awake(){
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO){
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.recipe){
            Transform newIconTemplate = Instantiate(iconTemplate, iconContainer);
            newIconTemplate.gameObject.SetActive(true);
            newIconTemplate.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
        recipeNameText.text = recipeSO.recipeName;
    }
}

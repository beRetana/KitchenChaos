using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlateIconsSingleUI : MonoBehaviour{

    [SerializeField] private Image iconImage;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO){
        iconImage.sprite = kitchenObjectSO.sprite;
    }
}
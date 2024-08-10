using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            //Theres in no kitchen object here
            if(player.HasKitchenObject()){
                //Player has a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else{
                //The player is not carrying anything
            }
        }
        else{
            //there is a kitchen object here
            if(player.HasKitchenObject()){
                //Player has a kitchen object
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)){
                    if(plateKitchenObject.TryAddToList(GetKitchenObject().GetKitchenObjectSO())){
                        GetKitchenObject().DestroySelf();
                    }
                }else{
                    //The player has a kitchen object that is not a plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)){
                        //The counter has a Plate on it
                        if(plateKitchenObject.TryAddToList(player.GetKitchenObject().GetKitchenObjectSO())){
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else{
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}

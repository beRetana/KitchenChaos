using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    [SerializeField] private float spawnPlateTimerMax;
    [SerializeField] private int spawnedPlateNumMax;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private int spawnedPlateNum;

    public event EventHandler OnSpawnPlate;
    public event EventHandler OnRemovePlate;

    private void Update(){
        spawnPlateTimer += Time.deltaTime;
        if(GameManager.Instance.IsGamePlaying() && spawnPlateTimer >= spawnPlateTimerMax){
            spawnPlateTimer = 0f;
            if(spawnedPlateNum < spawnedPlateNumMax){
                spawnedPlateNum++;

                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
            if(spawnedPlateNum > 0){
                KitchenObject.SpawKitchenObject(plateKitchenObjectSO, player);
                OnRemovePlate?.Invoke(this, EventArgs.Empty);
                spawnedPlateNum--;
            }
        }
    }
}


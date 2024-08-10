using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour{

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float spawnRecipeTimerMax;
    [SerializeField] private int waitingRecipesMax;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private int sucessfulRecipesDelivered;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance {get; private set; }

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        waitingRecipeSOList = new List<RecipeSO>();
        spawnRecipeTimer = spawnRecipeTimerMax;
    }

    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f){
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax){
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject){
        foreach (RecipeSO recipeSO in waitingRecipeSOList){
            if(recipeSO.recipe.Count == plateKitchenObject.GetKitchenObjectSOList().Count){
                //The recipe and the plate have the same amount of ingredients
                bool contentsMatch = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in recipeSO.recipe){
                    //Looping through the ingredients in the waiting recipe.
                    bool ingredientsMatch = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()){
                        //Looping through the ingredients in the plate.
                        if(plateKitchenObjectSO == recipeKitchenObjectSO){
                            ingredientsMatch = true;
                            break;
                        }
                    }
                    if(!ingredientsMatch){
                        //The ingredient in the recipe was not found in the plate.
                        contentsMatch = false;
                    }
                }
                if(contentsMatch){
                    sucessfulRecipesDelivered++;
                    waitingRecipeSOList.Remove(recipeSO);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeOSList(){
        return waitingRecipeSOList;
    }

    public int GetSucessfulRecipesDelivered(){
        return sucessfulRecipesDelivered;
    }
}

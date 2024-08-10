using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float timer, burningTimer;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs{
        public State state;
    }

    private State state;

    public enum State{
        Empty,
        Frying,
        Fried,
        Burned,
    }

    private void Start(){
        state = State.Empty;
    }

    private void Update(){
        if(HasKitchenObject()){
            switch (state){
                case State.Empty:
                    break;
                case State.Frying:
                    timer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = timer / fryingRecipeSO.fryingTimerMax
                    });
                    if(timer >= fryingRecipeSO.fryingTimerMax){
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });
                    if(burningTimer >= burningRecipeSO.burningTimerMax){
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                            progressNormalized = 0f
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
            //Theres in no kitchen object here
            if(player.HasKitchenObject()){
                //Player has a kitchen object
                if(HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    timer = 0f;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = timer / fryingRecipeSO.fryingTimerMax
                    });
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                    });
                }
                else{
                    //Player is carrying somthing that doesn't have a recipe
                }
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
                        state = State.Empty;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = 0f
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                        state = state
                        });
                    }
                }
            }
            else{
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Empty;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                    progressNormalized = 0f
                });
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                    state = state
                });
            }
        }
    }

    private bool HasRecipeForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO){
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null){
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray){
            if(fryingRecipeSO.input == inputKitchenObjectSO){
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO){
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray){
            if(burningRecipeSO.input == inputKitchenObjectSO){
                return burningRecipeSO;
            }
        }
        return null;
    }

    public bool IsFried(){
        return state == State.Fried;
    }
}

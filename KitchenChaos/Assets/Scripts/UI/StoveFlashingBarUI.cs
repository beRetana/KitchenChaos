using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveFlashingBarUI : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private Animator animator;

    private const string STOVE_WARNING_FLASHING_BAR = "IsFlashing";

    private void Start(){
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if(stoveCounter.IsFried() && e.progressNormalized >= 0.5f){
            animator.SetBool(STOVE_WARNING_FLASHING_BAR, true);
        }
        else{
            animator.SetBool(STOVE_WARNING_FLASHING_BAR, false);
        }
    }
}

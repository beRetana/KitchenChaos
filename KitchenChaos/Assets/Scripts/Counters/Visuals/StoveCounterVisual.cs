using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject heatingPlateStove, cookingParticles;

        private void Start(){
            stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisuals = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        heatingPlateStove.SetActive(showVisuals);
        cookingParticles.SetActive(showVisuals);
    }
}

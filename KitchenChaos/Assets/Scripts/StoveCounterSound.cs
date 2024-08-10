using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float stoveWarningTimer;
    private bool isBurning;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Start(){
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried){
            audioSource.Play();
        }
        else{
            audioSource.Pause();
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        isBurning = stoveCounter.IsFried() && e.progressNormalized >= .5f;
    }

    private void Update(){
        if(isBurning){
            stoveWarningTimer -= Time.deltaTime;
            if(stoveWarningTimer <= 0f){
                float stoveWarningTimerMax = .2f;
                stoveWarningTimer = stoveWarningTimerMax;
                SoundManager.Instance.PlayWarningSound(transform.position);
            }
        }
    }
}

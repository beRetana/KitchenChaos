using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    [SerializeField] private SoundClipRefsSO soundClipRefsSO;

    public static SoundManager Instance {get; private set;}

    private float volume = 1f;
    private const string PLAYER_PREFS_SFX_VOLUME = "SFXVolume";

    private void Awake(){
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectsPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySFX(soundClipRefsSO.objectDrop, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectsPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySFX(soundClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        Vector3 position = Player.Instance.transform.position;
        PlaySFX(soundClipRefsSO.objectPickup, position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySFX(soundClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e)
    {
        Vector3 position = DeliveryCounter.Instance.transform.position;
        PlaySFX(soundClipRefsSO.deliverySucess, position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        Vector3 position = DeliveryCounter.Instance.transform.position;
        PlaySFX(soundClipRefsSO.deliveryFailed, position);
    }

    private void PlaySFX(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f){
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySFX(AudioClip[] audioClip, Vector3 position, float volumeMultiplier = 1f){
        AudioSource.PlayClipAtPoint(audioClip[UnityEngine.Random.Range(0, audioClip.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume){
        PlaySFX(soundClipRefsSO.footStep, position, volume);
    }

    public void PlayCountDownSound(){
        PlaySFX(soundClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position){
        PlaySFX(soundClipRefsSO.warning, position);
    }

    public void ChangeVolume(){
        volume += .1f;
        if(volume > 1f){
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume(){
        return volume;
    }
}

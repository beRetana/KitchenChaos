using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour {

    [SerializeField] private float footStepTimerMax;

    private float footStepTimer;
    private Player player;

    private void Start(){
        player = GetComponent<Player>();
        footStepTimer = 0f;
    }

    private void Update(){
        footStepTimer += Time.deltaTime;
        if(footStepTimer >= footStepTimerMax){
            footStepTimer = 0f; 
            if(player.IsWalking()){
                SoundManager.Instance.PlayFootStepSound(player.transform.position, 1f);
            }
        }
    }
}

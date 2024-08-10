using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameInput gameInput;
    [SerializeField] private float countDownToStartTimerMax = 3f;
    [SerializeField] private float gamePlayingTimerMax = 4f;

    public static GameManager Instance {get; private set;}

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State{
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countDownToStartTimer;
    private float gamePlayingTimer;
    private bool isGamePaused;

    private void Awake(){
        Instance = this;

        state = State.WaitingToStart;

        countDownToStartTimer = countDownToStartTimerMax;
        gamePlayingTimer = gamePlayingTimerMax;
    }

    private void Start(){

        gameInput.OnPauseGame += GameInput_OnGamePause;
        gameInput.OnInteractAction += GameInput_OnInteractionAction;
    }

    private void GameInput_OnInteractionAction(object sender, EventArgs e)
    {
        if(state == State.WaitingToStart){
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update(){
        switch (state){
            case State.WaitingToStart: 
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if(countDownToStartTimer <= 0f){
                    countDownToStartTimer = countDownToStartTimerMax;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer <= 0f){
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

        //Debug.Log(state);
    }
    
    private void GameInput_OnGamePause(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame(){
        isGamePaused = !isGamePaused;
        if(isGamePaused){
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else{
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
    }

    public bool IsGamePlaying(){
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive(){
        return state == State.CountDownToStart;
    }

    public float GetCountDownToStartTimer(){
        return countDownToStartTimer;
    }

    public bool IsGameOver(){
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized(){
        return 1 - gamePlayingTimer/gamePlayingTimerMax;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private Animator animator;

    private const string COUNTDOWN_ANIMATION_TRIGGER = "CountDownPopUp";
    private int previousCountdownNumber;

    private void Start(){
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void Update(){
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
        countDownText.text = countdownNumber.ToString();
        if(previousCountdownNumber != countdownNumber){
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(COUNTDOWN_ANIMATION_TRIGGER);
            SoundManager.Instance.PlayCountDownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        gameObject.SetActive(GameManager.Instance.IsCountDownToStartActive());
    }
}

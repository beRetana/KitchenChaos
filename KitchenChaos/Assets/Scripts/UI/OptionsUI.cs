using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour{

    [SerializeField] private Button sFXOptionsButton, musicOptionsButton, closeButton, moveUpButton, moveDownButton,
                                    moveLeftButton, moveRightButton, interactButton, interactAltButton, pauseButton;
    [SerializeField] private Button gamepadInteractButton, gamepadInteractAltButton, gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI sFXOptionsText, musicOptionsText, moveUpText, moveDownText,
                                             moveLeftText, moveRightText, interactText, interactAltText, pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText, gamepadInteractAltText, gamepadPauseText;

    [SerializeField] private Transform rebindUI;

    private Action OnCloseButtonAction;

    private void Awake(){
        sFXOptionsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisuals();
        });
        musicOptionsButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();
        });
        closeButton.onClick.AddListener(() => {
            OnCloseButtonAction();
            Hide();
        });
        moveUpButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Interact);
        });
        interactAltButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Interact_Alt);
        });
        pauseButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Pause);
        });
        gamepadPauseButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Gamepad_Pause);
        });
        gamepadInteractButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Gamepad_Interact);
        });
        gamepadInteractAltButton.onClick.AddListener(() => {
            RebindingInputKeys(GameInput.Binding.Gamepad_InteractAlt);
        });
    }

    private void Start(){
        GameManager.Instance.OnGameUnpaused += GameManager_OnUnpausedGame;
        UpdateVisuals();
        Hide();
    }

    private void GameManager_OnUnpausedGame(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisuals(){
        sFXOptionsText.text = "SFX: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicOptionsText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();

        moveUpText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Interact_Alt);
        pauseText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Pause);
        gamepadPauseText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Gamepad_Pause);
        gamepadInteractText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBingingText(GameInput.Binding.Gamepad_InteractAlt);
    }

    public void Show(Action onCloseButtonAction){
        OnCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        sFXOptionsButton.Select();
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    public void ShowRebindingUI(){
        rebindUI.gameObject.SetActive(true);
    }

    public void HideRebindingUI(){
        rebindUI.gameObject.SetActive(false);
    }

    private void RebindingInputKeys(GameInput.Binding binding){
        ShowRebindingUI();
        GameInput.Instance.RebindBinding(binding, () => {
            HideRebindingUI();
            UpdateVisuals();
        });
    }
}

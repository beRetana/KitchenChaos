using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI moveUpText, moveDownText, moveLeftText, moveRightText, pauseText, leftstickText, gamepadInteractText,
                                             gamepadInteractAltText, gamepadPauseText, interactText, interactAltText;

    void Start(){
        UpdateVisuals();
        GameInput.Instance.OnRebindKey += GameInput_OnRebindKey;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsCountDownToStartActive()){
            Hide();
        }
    }

    private void GameInput_OnRebindKey(object sender, EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals(){
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

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

}  

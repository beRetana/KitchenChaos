using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerCounterVisuals : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private const string OPEN_CLOSE = "OpenClose";

    private Animator containerCounterAnimator;

    private void Awake(){
        containerCounterAnimator = GetComponent<Animator>();
    }

    private void Start(){
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e){
        containerCounterAnimator.SetTrigger(OPEN_CLOSE);
    }
}

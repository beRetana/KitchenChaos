using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private const string CUT = "Cut";

    private Animator cuttingCounterAnimator;

    private void Awake(){
        cuttingCounterAnimator = GetComponent<Animator>();
    }

    private void Start(){
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e){
        cuttingCounterAnimator.SetTrigger(CUT);
    }
}

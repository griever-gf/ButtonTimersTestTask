using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TimerPanelCloseEvent : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        //base.OnStateExit(animator, stateInfo, layerIndex, controller);
        Destroy(animator.gameObject);
    }
}

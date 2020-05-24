﻿using UnityEngine;

public class BlockState : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool("isBlock", false);
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }
}

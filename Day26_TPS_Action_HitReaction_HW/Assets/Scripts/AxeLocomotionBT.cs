﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeLocomotionBT : StateMachineBehaviour {
    public float moveSpeed = 3f;
    PlayerController pc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        pc.moveSpeed = moveSpeed;
        animator.SetBool("ComboAttack", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc.FrameMove();

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("ComboAttack", true);
        }
        if (Input.GetKeyDown(KeyCode.E) && pc.isEquipped && !animator.IsInTransition(0))
        {
            animator.SetTrigger("DropWeapon");
        }
        if (Input.GetKeyDown(KeyCode.X) && pc.isEquipped && !animator.IsInTransition(0))
        {
            animator.SetTrigger("Disarm");
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ComboAttack", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}

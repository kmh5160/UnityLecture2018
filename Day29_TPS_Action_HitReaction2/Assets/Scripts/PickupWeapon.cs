using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : StateMachineBehaviour {
    PlayerController pc;
    Transform weaponHolder;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pc = animator.GetComponent<PlayerController>();
        weaponHolder = pc.weaponHolder;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (weaponHolder.childCount == 0 && 0.22f > stateInfo.normalizedTime)
        {
            GameObject weapon = pc.GetNearestWeaponIn(radius: 1.5f, angle: 180f, weaponTag: "RightWeapon");
            if (weapon == null)
                return;
            //Destroy(weapon.GetComponent<Rigidbody>());
            weapon.GetComponent<Rigidbody>().isKinematic = true;
            foreach (var c in weapon.GetComponents<Collider>())
                c.enabled = false;
            weapon.transform.SetParent(weaponHolder.transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}

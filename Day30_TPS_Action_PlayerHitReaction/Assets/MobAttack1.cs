using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack1 : StateMachineBehaviour, IHitBoxResponder
{
    public int damage = 1;
    HitBox hitBox;
    Dictionary<int, int> hitObject;
    Vector3 knockBackDir;

    public void CollisionWith(Collider collider)
    {
        Debug.Log("Colliding: " + collider.name);
        HurtBox hurtBox = collider.GetComponent<HurtBox>();
        if (hurtBox != null)
        {
            hurtBox.GetHitBy(damage);
        }

        if (!hitObject.ContainsKey(collider.gameObject.GetInstanceID()))
            hitObject[collider.gameObject.GetInstanceID()] = 1;
        else
        {
            hitObject[collider.gameObject.GetInstanceID()] += 1;
            return;
        }

        HitReaction hr = collider.GetComponent<HitReaction>();
        if (hr != null)
            hr.TakeDamage(damage, "Reaction", knockBackDir);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox = animator.GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
        hitObject = new Dictionary<int, int>();
        knockBackDir = animator.transform.forward;
        hitBox.StartCheckingCollision();

        //animator.GetComponent<PlayerController>().weaponHolder.GetComponentInChildren<TrailRenderer>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stateInfo.normalizedTime >= 0.35f)
        //    hitBox.StartCheckingCollision();
        if (0.35f <= stateInfo.normalizedTime && stateInfo.normalizedTime <= 0.6f)
            hitBox.UpdateHitBox();
        //if (stateInfo.normalizedTime >= 0.43f)
        //    hitBox.StopCheckingCollision();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hitBox.StopCheckingCollision();
        //animator.GetComponent<PlayerController>().weaponHolder.GetComponentInChildren<TrailRenderer>().enabled = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour {
    public GameObject hitFXPrefab;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int amount, string reactionType, Vector3 knockBackDir)
    {
        if (anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
            anim.SetTrigger(reactionType);
        GetComponent<Health>().DecreaseHP(amount);
        GetComponent<Rigidbody>().velocity = knockBackDir * 2f;
        GameObject clone = Instantiate(hitFXPrefab, transform.position, Quaternion.LookRotation(-knockBackDir));
        Destroy(clone, 1.5f);
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"));
    }
}

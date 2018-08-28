using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour {
    public GameObject hitFXPrefab;
    Animator anim;
    Rigidbody rb;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        //if (anim = null)
            anim = GetComponentInParent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (rb = null)
            rb = GetComponentInParent<Rigidbody>();
    }

    public void TakeDamage(int amount, string reactionType, Vector3 knockBackDir)
    {
        if (anim != null)
            anim.SetTrigger(reactionType);
        //GetComponent<Health>().DecreaseHP(amount);
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
        GetComponentInParent<Health>().DecreaseHP(amount);

        if (rb != null)
            rb.velocity = knockBackDir * 2f;
        GameObject clone = Instantiate(hitFXPrefab, transform.position, Quaternion.LookRotation(-knockBackDir));
        Destroy(clone, 1.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour {

    public int Damage = 1;
    Rigidbody rb;
    EnemyControler ec;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Untagged")
            Destroy(gameObject);
        if (!(collision.tag == "Enemy"))
            return;
        Vector3 dir = rb.velocity;
        ec = collision.GetComponentInParent<EnemyControler>();
        ec.GetDamage(Damage);
        ec.KnockBack(dir,3f);
        Destroy(gameObject);
    }
}
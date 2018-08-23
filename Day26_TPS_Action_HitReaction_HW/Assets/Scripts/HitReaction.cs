using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour {
    public GameObject hitFXPrefab;

    public void TakeDamage(int amount, Vector3 knockBackDir)
    {
        GetComponent<Health>().DecreaseHP(amount);
        GetComponent<Rigidbody>().velocity = knockBackDir * 2f;
        GameObject clone = Instantiate(hitFXPrefab, transform.position, Quaternion.LookRotation(-knockBackDir));
        Destroy(clone, 1.5f);
    }
}

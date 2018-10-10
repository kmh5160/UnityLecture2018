using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HitReaction : MonoBehaviour {
    public GameObject hitFXPrefab;

    Rigidbody rb;
    Health health;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        health.OnDeath += Die;
	}

    private void Die()
    {
        print("Die!!");
        var f = Instantiate(hitFXPrefab, transform.position, Quaternion.AngleAxis(-90, Vector3.right));
        Destroy(f, 3f);
        transform.Find("PushBox").GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<Jumping>().loopOn = false;
        transform.Find("Model").GetComponent<MeshRenderer>().material.color = Color.red;
        rb.AddForce(Vector3.right * 10f, ForceMode.VelocityChange);
        transform.DORotate(-Vector3.forward * 360f, 2, RotateMode.FastBeyond360).OnComplete(() =>
        {
            print("completed!");
        });
    }

    public void Beaten()
    {
        health.DecreaseHP(10);
        StartCoroutine(KnockBack(transform.right, 0.4f, 0.05f));
    }

    IEnumerator KnockBack(Vector3 dir, float distance, float duration)
    {
        float d = 0;
        while (d < distance)
        {
            rb.velocity = Vector3.zero;
            rb.MovePosition(transform.position + dir * distance / duration * Time.fixedDeltaTime);
            d += distance / duration * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class javelinController : MonoBehaviour, IHitBoxResponder {
    public float throwingForce = 35f;

    Rigidbody rb;
    HitBox hitBox;

	// Use this for initialization
	void Awake () {
        rb.GetComponent<Rigidbody>();
        hitBox = GetComponentInChildren<HitBox>();
        hitBox.SetResponder(this);
        hitBox.StartCheckingCollision();
	}
	
	public void Throw()
    {
        rb.AddForce(transform.right * throwingForce, ForceMode.VelocityChange);
        transform.DORotate(-transform.forward * 80f, 2f);
    }

    private void Update()
    {
        hitBox.UpdateHitBox();
    }

    public void CollisionedWith(Collider collider)
    {
        rb.isKinematic = true;
        DOTween.Kill(transform);
        transform.SetParent(collider.gameObject.transform);
        hitBox.StopCheckingCollision();
    }
}

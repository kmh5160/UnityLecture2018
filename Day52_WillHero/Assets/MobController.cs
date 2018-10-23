using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour, IHitBoxResponder {
    HitBox hitbox;

    public void CollisionedWith(Collider collider)
    {
        PlayerController pc = collider.gameObject.GetComponentInParent<PlayerController>();
        pc.Beaten();
        hitbox.StopCheckingCollision();
    }

    // Use this for initialization
    void Start () {
        hitbox = GetComponentInChildren<HitBox>();
        hitbox.SetResponder(this);
        hitbox.StartCheckingCollision();
	}
	
	// Update is called once per frame
	void Update () {
        hitbox.UpdateHitBox();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    bool onGround;
    float jumpPressure;
    public float minJump = 2f;
    public float maxJump = 10f;

    Rigidbody rb;
    Animator anim;

	// Use this for initialization
	void Start () {
        onGround = true;
        jumpPressure = 0f;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround)
        {
            if (Input.GetButton("Jump"))
            {
                if (jumpPressure < maxJump)
                    jumpPressure += 10f * Time.deltaTime;
                else
                    jumpPressure = maxJump;

                anim.SetFloat("jumpPressure", jumpPressure + minJump);
                anim.speed = 1f + (jumpPressure / 10f);
            }
            else
            {
                if (jumpPressure > 0f)
                {
                    jumpPressure += minJump;
                    rb.velocity = new Vector3(0f, jumpPressure, 0f);
                    jumpPressure = 0f;
                    onGround = false;
                    anim.SetFloat("jumpPressure", jumpPressure);
                    anim.SetBool("onGround", onGround);
                }
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            anim.SetBool("onGround", onGround);
        }
    }
}

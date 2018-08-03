using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {
    public float speed = 10f;
    public float rotateSpeed = 10f;
    Rigidbody rb;
    Vector3 move;
    Animator anim;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turn(h, v);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            anim.SetBool("Move", true);
        else
            anim.SetBool("Move", false);
    }

    void Move(float h, float v)
    {
        move.Set(h, 0, v);
        move = move.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }

    void Turn(float h, float v)
    {
        if (h == 0 && v == 0)
            return;
        Quaternion qt = Quaternion.LookRotation(move);
        rb.rotation = Quaternion.Slerp(rb.rotation, qt, rotateSpeed * Time.deltaTime);
        rb.MoveRotation(qt);
    }
}

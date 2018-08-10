using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 3f;
    public float rotationSpeed = 150f;

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;
    Animator anim;
    float jumpSpeed = 8f;
    float gravity = 23f;
    float currentDownSpeed;

    MouseLook mouseLook;

	// Use this for initialization
	void Start () {
        con = GetComponent<CharacterController>();
        mouseLook = GetComponentInChildren<MouseLook>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            moveDirection = (new Vector3(h, 0, v)).normalized;
            float rotationY = mouseMoveX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationY);
            mouseLook.ResetCamera();

            if (Input.GetKey(KeyCode.A))
            {
                anim.SetBool("Left", true);
                anim.SetBool("Right", false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
            }
            else
            {
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
            }
        }
        else
        {
            moveDirection = (new Vector3(0, 0, v)).normalized;
            float rotationY = h * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationY);
        }

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= 1.5f * moveSpeed;
        anim.SetFloat("Speed", con.velocity.magnitude);

        if (Input.GetButtonDown("Jump"))
            jump();

        currentDownSpeed -= gravity * Time.deltaTime;
        moveDirection.y = currentDownSpeed;

        con.Move(moveDirection * Time.deltaTime);
    }

    private void jump()
    {
        if (!con.isGrounded)
            return;
        currentDownSpeed = jumpSpeed;
    }
}

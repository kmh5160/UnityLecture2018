using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour {
    public float jumpHeight = 4f;
    public BoxCollider groundCheckBox;
    public LayerMask groundMask;
    public bool drawGizmo = true;
    public bool loopOn = true;
    public bool isGrounded = false;

    Rigidbody rb;
    bool queuedJumpingForce = false;
    ColliderState state;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void OnDrawGizmos()
    {
        if (!drawGizmo)
            return;

        CheckGizmoColor();
        Gizmos.matrix = groundCheckBox.transform.localToWorldMatrix;
        Gizmos.DrawCube(groundCheckBox.center, groundCheckBox.size);
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Open:
                Gizmos.color = Color.green;
                break;
            case ColliderState.Colliding:
                Gizmos.color = Color.magenta;
                break;
        }
    }

    private void FixedUpdate()
    {
        bool overlapsGround;
        overlapsGround = Physics.CheckBox(groundCheckBox.transform.TransformPoint(groundCheckBox.center),
                                          groundCheckBox.size * 0.5f,
                                          groundCheckBox.transform.rotation,
                                          groundMask,
                                          QueryTriggerInteraction.Ignore);
        if (overlapsGround)
        {
            RaycastHit hit;
            bool isOnGround = Physics.BoxCast(groundCheckBox.transform.TransformPoint(groundCheckBox.center),
                                              groundCheckBox.size * 0.5f * 0.9f,
                                              Vector3.down,
                                              out hit,
                                              groundCheckBox.transform.rotation,
                                              0.2f,
                                              groundMask,
                                              QueryTriggerInteraction.Ignore);
            isGrounded = isOnGround;
        }
        else
            isGrounded = false;
        //isGrounded = overlapsGround;

        if (isGrounded)
            state = ColliderState.Colliding;
        else
            state = ColliderState.Open;

        if (isGrounded && loopOn && !queuedJumpingForce)
        {
            Invoke("Jump", 0.15f);
            queuedJumpingForce = true;
        }
    }

    void Jump()
    {
        queuedJumpingForce = false;
        if (!isGrounded)
            return;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // only y value
        //rb.velocity += Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
}

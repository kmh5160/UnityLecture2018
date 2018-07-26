using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMove : MonoBehaviour {
    public float moveSpeed = 8f;
    public GameObject healFX;
    public float slideSpeed = 3f;

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;
    float jumpSpeed = 8f;
    float gravity = 20f;

    bool healing = false;
    GameObject fx;

    bool isOnSlope = false;
    Vector3 hitNormal;
    Vector3 hitPoint;

    void Start () {
        con = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float ftemp = moveDirection.y;
        moveDirection = (new Vector3(h, 0, v)).normalized;
        transform.LookAt(transform.position + moveDirection);
        moveDirection *= moveSpeed;
        moveDirection.y = ftemp;

        if (con.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            //if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) > con.slopeLimit;
        Vector3 slideDirection = Vector3.zero;
        if (isOnSlope)
        {
            slideDirection = (new Vector3(hitNormal.x, 0f, hitNormal.z)) * slideSpeed;
            //Vector3 c = Vector3.Cross(hitNormal, Vector3.up);
            //slideDirection = Vector3.Cross(hitNormal, c) * slideSpeed;
            Debug.DrawRay(hitPoint, slideDirection, Color.magenta);
        }

        con.Move((moveDirection + slideDirection) * Time.deltaTime);
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Step3")
        {
            if (!healing)
            {
                fx = Instantiate(healFX, transform.Find("FXPos"));
                healing = true;
                Invoke("RemoveHealFX", 1.9f);
            }
        }

        hitNormal = hit.normal;
        hitPoint = hit.point;

    }

    void RemoveHealFX()
    {
        Destroy(fx);
        healing = false;
    }
}

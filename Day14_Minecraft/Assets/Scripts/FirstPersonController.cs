using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    public float moveSpeed = 8f;
    public float jumpForce = 250f;
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    public GameObject healFX;

    Vector3 moveDirection = Vector3.zero;

    Rigidbody rb;
    bool isHealing = false;
    private bool grounded = false;
    private GameObject fx;

    Transform cameraTransform;
    float verticalLookRotation;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 + 0.1f))
            grounded = true;
        else
            grounded = false;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            //rb.velocity = false;
            rb.AddForce(transform.up * jumpForce);
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (new Vector3(h, 0, v)).normalized;
        moveDirection *= moveSpeed;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -75, 60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
                
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = transform.TransformDirection(moveDirection) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Step3")
        {
            if (!isHealing)
            {
                fx = Instantiate(healFX, transform.Find("FXPos"));
                isHealing = true;
                Invoke("RemoveHealFX", 1.9f);
            }
        }
    }

    void RemoveHealFX()
    {
        Destroy(fx);
        isHealing = false;
    }

}

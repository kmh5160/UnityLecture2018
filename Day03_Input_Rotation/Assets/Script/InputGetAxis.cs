using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGetAxis : MonoBehaviour {
    public float speed = 10f;
    public float rotationSpeed = 120f;
	
	void Update () {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        //print("v: " + Input.GetAxisRaw("Vertical"));

        //transform.Translate(0, 0, v * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
        //transform.Rotate(0, h * rotationSpeed * Time.deltaTime, 0);
        //transform.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime, 0);
        transform.rotation *= Quaternion.AngleAxis(h * rotationSpeed * Time.deltaTime, Vector3.up);
    }
}

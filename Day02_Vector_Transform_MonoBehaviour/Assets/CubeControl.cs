using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Hello Unity!");
	}
	
	// Update is called once per frame
	void Update () {
        if (name == "Cube") {
            if (Input.GetKey(KeyCode.W))
                transform.Rotate(-Vector3.left * 5f);
            if (Input.GetKey(KeyCode.S))
                transform.Rotate(Vector3.left * 5f);
        }

        if (name == "Root")
        {
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward * 0.1f);
            if (Input.GetKey(KeyCode.S))
                transform.Translate(-Vector3.forward * 0.1f);
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.down * 5f);
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(Vector3.up * 5f);
        }
    }
}

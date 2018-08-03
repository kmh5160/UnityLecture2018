using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 0.1f);
            transform.Rotate(Vector3.forward);
        }
            
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * 0.1f);
            transform.Rotate(-Vector3.forward);
        }
            
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * 0.1f);
            transform.Rotate(-Vector3.right);
        }
            
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 0.1f);
            transform.Rotate(Vector3.right);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.forward * 5f);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.forward * 5f);
    }
}

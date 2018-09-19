using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour {
    public float speed;
    float x;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        x -= Time.deltaTime * speed;
		transform.rotation = Quaternion.Euler(x, 90, 90);
    }
}

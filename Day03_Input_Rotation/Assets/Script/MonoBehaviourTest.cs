using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourTest : MonoBehaviour {

    private void Awake()
    {
        print("Awake()");
    }

    // Use this for initialization
    void Start () {
        print("Start()");
        print(name == "Cube");
        print(gameObject.name == name);
        print(transform == GetComponent<Transform>());
        print(GetComponent<MonoBehaviourTest>() == this);
	}
	
	// Update is called once per frame
	void Update () {
        print("Update()");
    }

    private void FixedUpdate()
    {
        print("FixedUpdate()");
    }

    private void LateUpdate()
    {
        print("LateUpdate()");
    }
}

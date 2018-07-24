using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print(transform.position);
        print(transform.rotation);
        print(transform.lossyScale);

        print(transform.forward);
        print(transform.right);
        print(transform.up);

        print(transform.childCount == 3);
        print(transform.GetChild(0).name == "B");
        print(transform.GetChild(0).parent.name == "A");
        print(transform.Find("D").name == "D");
        print(transform.Find("D/E").name == "E");
        print(transform.Find("D/E").root == transform);
        print(transform.Find("D/E").root.name == transform.name);

        print(transform.GetComponent<Transform>() == transform);

        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

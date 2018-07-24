using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        //while (transform.position != new Vector3(10.0f, 0.0f, 0.0f)) {
        //    transform.Translate(Vector3.right * 0.1f);
        //}
        Vector3 v1 = new Vector3(5.0f, 0.0f, 0f);
        Vector3 v2 = new Vector3(5.0f, 5.0f, 0f);
        Vector3 v3 = new Vector3(0.0f, 5.0f, 0f);
        Vector3 v4 = new Vector3(0.0f, 0.0f, 0f);

        if (Mathf.Round(transform.position.x) < v1.x && Mathf.Round(transform.position.y) == v1.y)
        {
            transform.Translate(Vector3.right * 0.1f);
            print(transform.position);
        }
        else if (Mathf.Round( transform.position.x ) == v2.x && Mathf.Round(transform.position.y) < v2.y)
        {
            transform.Translate(Vector3.up * 0.1f);
            print(transform.position);
        }
        else if (Mathf.Round(transform.position.x ) > v3.x && Mathf.Round(transform.position.y ) == v3.y)
        {
            transform.Translate(Vector3.left * 0.1f);
        }
        else if (Mathf.Round(transform.position.y ) > v4.y && Mathf.Round(transform.position.x ) == v4.x)
        {
            transform.Translate(Vector3.down * 0.1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public int speed;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            //GetComponent<Rigidbody>().AddForce(transform.right * 1000f);
            anim.SetTrigger("Move");
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + new Vector3(3, 0, 0));
            //transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right * 3f, speed * Time.deltaTime);
        }
	}
}

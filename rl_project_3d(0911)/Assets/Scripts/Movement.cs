using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float MoveSpeed = 150f;

    private Vector3 dir;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        PlayerController();
    }

    private void PlayerController()
    {
        float h = Input.GetAxisRaw( "Horizontal" );
        float v = Input.GetAxisRaw( "Vertical" );

        dir = new Vector3( h, 0, v ).normalized;

        Vector3 movement = dir * MoveSpeed * Time.fixedDeltaTime;
        //transform.position += movement;
        GetComponent<Rigidbody>().velocity = movement;

        //print( "speed:"+ v );
    }

}

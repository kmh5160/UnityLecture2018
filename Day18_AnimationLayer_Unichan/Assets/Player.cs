using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject Bullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g = Instantiate(Bullet, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(Vector3.forward * 400f * Time.deltaTime);
            Destroy(this.gameObject, 3f);
        }
    }
}

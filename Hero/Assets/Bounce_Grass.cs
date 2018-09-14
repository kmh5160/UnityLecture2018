using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_Grass : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            print("Bounce!");
            collision.collider.GetComponent<Rigidbody>().AddForce(transform.up * 300f);
        }
    }
}

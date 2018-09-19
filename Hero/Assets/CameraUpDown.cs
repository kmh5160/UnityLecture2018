using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpDown : MonoBehaviour {
    public Camera camera;
    public float x, y, z;
    //string Land_name;

    private void OnCollisionEnter(Collision collision)
    {
        //Land_name = collision.collider.name;
        if (collision.collider.tag == "Land")
        {
            print("Bounce!");
            x = GetComponentInParent<Transform>().position.x + 5;
            y = collision.collider.GetComponent<Transform>().position.y + 3;
            z = -10;
            //camera.transform.position = new Vector3(x, y, z);
        }
    }
}

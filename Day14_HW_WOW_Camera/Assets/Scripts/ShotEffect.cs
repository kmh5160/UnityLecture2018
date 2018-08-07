using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEffect : MonoBehaviour {
    public GameObject ShotFX;
    GameObject fx;

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    print("Hit");
    //    fx = Instantiate(ShotFX, transform);
    //    Destroy(fx, 3f);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //print("Hit");
        fx = Instantiate(ShotFX, transform.position, Quaternion.identity);
        Destroy(fx, 1f);
    }
}

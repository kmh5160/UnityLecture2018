using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public GameObject Bullet;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameObject g = Instantiate(Bullet, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(transform.forward * 5000f);
            Destroy(g.gameObject, 3f);
        }
    }
}

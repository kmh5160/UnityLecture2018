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
        if (Input.GetKey(KeyCode.Z))
        {
            GameObject g = Instantiate(Bullet, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(transform.forward * 50000f * Time.deltaTime);
            Destroy(g.gameObject, 3f);
        }
    }
}

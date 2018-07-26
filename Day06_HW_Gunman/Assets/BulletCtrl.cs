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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g = Instantiate(Bullet, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(Vector3.right * 20000f * Time.deltaTime);
            Destroy(g.gameObject, 3f);
        }
    }
}

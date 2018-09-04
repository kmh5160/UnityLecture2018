using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseCtrl : MonoBehaviour
{
    public GameObject Case;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GameObject g = Instantiate(Case, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(transform.right * 200f);
            Destroy(g.gameObject, 3f);
        }
    }
}

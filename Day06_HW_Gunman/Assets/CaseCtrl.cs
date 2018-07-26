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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g = Instantiate(Case, transform.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().AddForce(Vector3.right * 5000f * Time.deltaTime);
            Destroy(g.gameObject, 3f);
        }
    }
}

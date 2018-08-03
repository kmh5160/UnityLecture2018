using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour {
    Vector3 gunDefaultPos;
    int delay = 0;
    public int setDelay = 5;
    public GameObject Bullet;
    public GameObject Case;

    // Use this for initialization
    void Start () {
        
	}

    void Awake()
    {
        gunDefaultPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(delay <= 0)
            {
                delay = setDelay;
                transform.localPosition = gunDefaultPos;
                transform.Translate(Vector3.back * 0.3f);

                GameObject g1 = Instantiate(Bullet, transform.Find("ShootPoint").position, Quaternion.identity);
                g1.GetComponent<Rigidbody>().AddForce(transform.Find("ShootPoint").forward * 5000f);
                Destroy(g1.gameObject, 3f);
                GameObject g2 = Instantiate(Case, transform.Find("CasePoint").position, Quaternion.identity);
                g2.GetComponent<Rigidbody>().AddForce(transform.Find("CasePoint").right * 200f);
                Destroy(g2.gameObject, 3f);


            }
            
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, gunDefaultPos, Time.deltaTime * 3.0f);

        if (Vector3.Distance(transform.localPosition, gunDefaultPos) < 0.001f)
        {
            transform.localPosition = gunDefaultPos;
        }
        if (0 < delay)
        {
            delay--;
        }
    }
    
}

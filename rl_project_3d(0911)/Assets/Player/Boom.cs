using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {

    public float startRadius;
    public float maxRadius;
    public float durationTime;
    SphereCollider col;

    float gap;
    // Use this for initialization
    void Start () {
        col = GetComponent<SphereCollider>();
        col.radius = startRadius;
        gap = maxRadius - startRadius;
        Destroy(gameObject, durationTime);
    }
	
	// Update is called once per frame
	void Update () {
        col.radius += gap * Time.deltaTime / durationTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponentInParent<Animator>().SetBool("isStun",true);
            collision.GetComponentInParent<EnemyControler>().DisableAgent();
        }
    }
}
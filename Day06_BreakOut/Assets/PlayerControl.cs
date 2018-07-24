using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    float speed = 15f;
    float speedX;

	void Update () {
        float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        speedX = h;
        Vector3 pos = transform.position + Vector3.right * h;
        transform.position = new Vector3(Mathf.Clamp(pos.x, -3.1f, 3.1f), pos.y, pos.z);
	}

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 400f * speedX);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float dashDistance = 3f;
    public bool dashOn = true;
    public Transform weaponHolder;
    public GameObject javelinePrefab;

    Rigidbody rb;
    Coroutine coDash;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (coDash != null)
                StopCoroutine(coDash);
            if (dashOn)
                coDash = StartCoroutine(Dash(dashDistance, 0.1f));
            FireWeapon();
        }

    }

    private void FireWeapon()
    {
        GameObject weaponBullet = Instantiate(javelinePrefab, weaponHolder.transform.position, Quaternion.AngleAxis(30, Vector3.forward));
        weaponBullet.GetComponent<javelinController>().Throw();
        Destroy(weaponBullet, 10f);
    }

    IEnumerator Dash(float distance, float duration)
    {
        float d = 0;
        while (d < distance)
        {
            rb.velocity = Vector3.zero;
            rb.MovePosition(transform.position + Vector3.right * distance / duration * Time.fixedDeltaTime);
            d += distance / duration * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    public float dashDistance = 3f;
    public bool playableOn = false;
    public bool dashOn = true;
    public Transform weaponHolder;
    public GameObject javelinePrefab;

    Rigidbody rb;
    Coroutine coDash;
    EZObjectPool objectPool;

    public event System.Action OnDeath;

    Jumping jumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectPool = GetComponent<EZObjectPool>();
        jumping = GetComponent<Jumping>();
    }

    void Update()
    {
        if (playableOn && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (coDash != null)
                StopCoroutine(coDash);
            if (dashOn)
                coDash = StartCoroutine(Dash(dashDistance, 0.15f));
            FireWeapon();
        }

    }

    private void FireWeapon()
    {
        //GameObject weaponBullet = Instantiate(javelinePrefab, weaponHolder.transform.position, weaponHolder.transform.rotation);
        //Quaternion.AngleAxis(30, Vector3.forward)
        //weaponBullet.GetComponent<JavelinController>().Throw();
        //Destroy(weaponBullet, 10f);

        GameObject weaponBullet;
        if (objectPool.TryGetNextObject(weaponHolder.transform.position, weaponHolder.transform.rotation, out weaponBullet))
        {
            weaponBullet.GetComponent<JavelinController>().Throw();
        }
        else
        {
            print("There is no pooled object");
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            RaycastHit hit;
            Ray r = new Ray(transform.position, Vector3.right);
            Debug.DrawLine(transform.position, transform.position + Vector3.right, Color.magenta, 1f);
            bool isInFrontOfMe = Physics.Raycast(r, out hit, 1f, 1 << LayerMask.NameToLayer("PushBoxMob"));
            if (isInFrontOfMe)
            {
                StartCoroutine(KnockBack(-transform.right, 2f, 0.1f));
            }
        }
    }

    IEnumerator KnockBack(Vector3 dir, float distance, float duration)
    {
        float d = 0;
        while(d < distance)
        {
            print("knock");
            rb.velocity = Vector3.zero;
            rb.MovePosition(transform.position + dir * distance / duration * Time.fixedDeltaTime);
            d += distance / duration * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    internal void RingOut()
    {
        if (OnDeath != null)
            OnDeath();
        //transform.position = new Vector3(3, 5, 0);
    }

    internal void Beaten()
    {
        Death();
        if (OnDeath != null)
            OnDeath();
    }

    void Death()
    {
        jumping.loopOn = false;
        transform.Find("Model").DOScaleY(0.2f, 0.5f);
    }
}

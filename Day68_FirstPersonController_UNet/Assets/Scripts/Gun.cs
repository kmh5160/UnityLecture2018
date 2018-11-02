using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : NetworkBehaviour {
    public GameObject weapon;
    public GameObject shellPrefab;
    public Transform shellEjection;
    public float fireRate = 10;
    public Light muzzleFlash;
    public GameObject impactFX;
    public GameObject bulletHolePrefab;

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos;
    Vector3 smoothVel;

    private void Awake()
    {
        fpsCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        originPos = weapon.transform.localPosition;
    }

    void Update ()
    {
        if (!isLocalPlayer)
            return;

		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            CmdShoot();
        }

        
	}

    [Command]
    private void CmdShoot()
    {
        muzzleFlash.enabled = true;
        Invoke("OffMuzzleFlash", 0.05f);
        MakeShell();

        CheckHit();

        //Shooting Sound
        GetComponent<AudioSource>().Play();

        //GameObject fx = Instantiate(impactFX, hit.point, Quaternion.identity);
        //Destroy(fx, 0.3f);

        //MakeBulletHole(hit.point, hit.normal, hit.transform);

        weapon.transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);
    }

    void CheckHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 200f))
        {
            //print(hit.transform.name);
            NetworkIdentity ni = hit.transform.gameObject.GetComponentInParent<NetworkIdentity>();
            if (ni != null)
                RpcHitReaction(ni.netId, hit.point, hit.normal);
        }
    }

    [ClientRpc]
    private void RpcHitReaction(NetworkInstanceId id, Vector3 point, Vector3 normal)
    {
        GameObject hit = ClientScene.FindLocalObject(id);
        Rigidbody rb = hit.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(fpsCamera.transform.forward * 500f);
        //hit.transform.GetComponent<AudioSource>().Play();
        BulletSound bs = hit.transform.GetComponent<BulletSound>();
        if (bs != null)
            bs.Play();
    }

    private void MakeBulletHole(Vector3 point, Vector3 normal, Transform parent)
    {
        GameObject clone = Instantiate(bulletHolePrefab, point + normal * 0.02f, Quaternion.FromToRotation(-Vector3.forward, normal), parent);
        clone.transform.SetParent(parent, true);
        Destroy(clone, 3f);
    }

    void OffMuzzleFlash()
    {
        muzzleFlash.enabled = false;
    }

    private void MakeShell()
    {
        GameObject clone = Instantiate(shellPrefab, shellEjection);
        //clone.transform.parent = null;
        clone.transform.SetParent(null);
    }

    private void LateUpdate()
    {
        weapon.transform.localPosition = Vector3.SmoothDamp(weapon.transform.localPosition, originPos, ref smoothVel, 0.1f);
    }
}

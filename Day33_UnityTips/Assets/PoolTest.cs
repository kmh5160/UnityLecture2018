using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class PoolTest : MonoBehaviour {
    public EZObjectPool objectPool;

    GameObject outObject;

	void Start () {
        if (objectPool.TryGetNextObject(Vector3.zero, Quaternion.identity, out outObject))
        {
            print(objectPool.AvailableObjectCount() == 9);
            print(objectPool.ActiveObjectCount() == 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(objectPool.AvailableObjectCount() == 9);
            print(objectPool.ActiveObjectCount() == 1);
            outObject.SetActive(false);
            print(objectPool.AvailableObjectCount() == 10);
            print(objectPool.ActiveObjectCount() == 0);
        }
	}
}

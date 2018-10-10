using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMode : MonoBehaviour {

    [SerializeField]
    bool isTestMode = false;

	// Use this for initialization
	void Start () {
        GameObject gameFlow = GameObject.Find("/GameFlow");
        isTestMode = gameFlow == null ? true : false;

        if (isTestMode)
        {
            Physics.gravity *= 4f;
        }
        else
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

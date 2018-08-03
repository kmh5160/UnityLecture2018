using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour {
    public AudioSource[] clips;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int rnd = Random.Range(0, clips.Length);
	}
}

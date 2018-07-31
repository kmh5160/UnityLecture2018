using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour {
    Animator anim;
    bool isOpened = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	public void ToggleDoor()
    {
        isOpened = !isOpened;
        anim.SetBool("isOpened", isOpened);
    }
}

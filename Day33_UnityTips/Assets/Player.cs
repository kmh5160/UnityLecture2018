using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print(MobManager.Instance.myGlovalVar == "whatever");
        print(MobManager.Instance.MobCount() == 100);
	}

}

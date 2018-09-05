using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    float Second = 99f;

	void Update () {
        if (Second <= 0)
            return;
        Second -= Time.deltaTime;
        //new WaitForSeconds(1);
        GetComponentInChildren<Text>().text = ((int)Second).ToString();
	}
}

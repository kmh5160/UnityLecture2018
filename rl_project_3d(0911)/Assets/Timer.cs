using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float timer = 181f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
            return;
        timer -= Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;

        string niceTime = string.Format("{0:0}:{1:00}", (int)minutes, (int)seconds);

        GetComponentInChildren<Text>().text = niceTime;
    }
}

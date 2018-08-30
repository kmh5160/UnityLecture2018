using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineReturn : MonoBehaviour {

    IEnumerator Start()
    {
        int ret = 10;
        yield return Todo("String", x => ret = x);
        print(ret);
    }

    IEnumerator Todo(string str, Action<int> result)
    {
        result(100); // ret = 100;
        yield return null;
    }
}
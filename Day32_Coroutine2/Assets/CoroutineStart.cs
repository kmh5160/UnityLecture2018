using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStart : MonoBehaviour {

    IEnumerator Start()
    {
        StartCoroutine(GetIdenticonFromURL());
        print("Start()");
        yield return StartCoroutine(Todo());
        print("C");
    }

    IEnumerator Todo()
    {
        print("A");
        yield return new WaitForSeconds(1f);
        print("B");
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator GetIdenticonFromURL()
    {
        string url = "http://www.gravatar.com/avatar/ctkim?s=256&d=retro&r=PG";

        using (WWW www = new WWW(url))
        {
            yield return www;
            var head = transform;
            head.GetComponent<Renderer>().material.mainTexture = www.texture;
        }
    }
}

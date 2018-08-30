using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineStop : MonoBehaviour {
    Coroutine co;

    IEnumerator Start()
    {
        co = StartCoroutine(Todo(2.0f));
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Todo(float value)
    {
        while (true)
        {
            print("Todo with " + value);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(co);
        }
    }

    //IEnumerator Start () {
    //       StartCoroutine("Todo", 2.0f);
    //       yield return new WaitForSeconds(1f);
    //       StopCoroutine("Todo");
    //}

    //IEnumerator Todo(float value)
    //   {
    //       while (true)
    //       {
    //           print("Todo with " + value);
    //           yield return new WaitForSeconds(0.2f);
    //       }
    //}
}

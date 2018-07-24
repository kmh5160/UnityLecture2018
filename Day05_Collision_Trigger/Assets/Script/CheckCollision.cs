using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {
    // OnCollisionEnter 발생 조건
    // 1. 두 개의 gameObject 모두 collider component가 존재해야 한다.
    // 2. 둘 중 하나는 rigidbody component가 있어야 한다.
    // 3. 그리고 rigidbody를 가진 gameObject가 움직여야 충돌되었을 때 발생한다. 그 반대는 10%로 발생한다.

    private void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter: " + collision.gameObject.name);
        foreach(ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.magenta, 5f);
        }
    }
}

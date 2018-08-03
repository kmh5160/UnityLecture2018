using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour {
    public GameObject healFX;
    bool healing = false;
    public GameObject target;
    GameObject fx;

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //if (hit.collider.gameObject.name == "Player")
    //{
    //if (!healing)
    //{
    //    print("Heal!");
    //    fx = Instantiate(healFX, target.transform.Find("FXPos"));
    //    healing = true;
    //    Invoke("RemoveHealFX", 1.9f);
    //}
    //}
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (!healing)
        {
            print("Heal!");
            fx = Instantiate(healFX, collision.collider.gameObject.transform.Find("FXPos"));
            healing = true;
            Invoke("RemoveHealFX", 1.9f);
        }
    }

    void RemoveHealFX()
    {
        Destroy(fx);
        healing = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTemp : MonoBehaviour {

    Damageable dmg;

    private void Start()
    {
        dmg = GetComponent<Damageable>();
    }

    private void OnCollisionEnter( Collision collision )
    {
        dmg.TakeDamage( 10 );
        if(dmg.currnt_hp <= 0)
        {
            Destroy( gameObject, 3f );
        }

    }

}

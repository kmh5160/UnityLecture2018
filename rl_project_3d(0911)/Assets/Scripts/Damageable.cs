using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    public float max_hp;
    public float currnt_hp;

    public GameObject hp_bar;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        currnt_hp = max_hp;
    }

    public void TakeDamage(int value)
    {
        if(currnt_hp > value)
        {
            currnt_hp -= value;
            if( hp_bar != null )
            {
                hp_bar.transform.localScale = new Vector3( (currnt_hp / max_hp), 1, 1 );
                print( "hp_bar : " + (currnt_hp / max_hp) );
            }
            print( "hp :" + currnt_hp );

        } else if( currnt_hp > 0 )
        {
            currnt_hp = 0;
            if( hp_bar != null )
            {
                hp_bar.transform.localScale = new Vector3( 0, 1, 1 );
            }
            
            print( "dead" );
        } else
        {

        }
    }


}

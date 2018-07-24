using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    public ParticleSystem effect;

    private void OnCollisionEnter(Collision collision)
    {
        var e = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(e.gameObject, 3f);
        Destroy(this.gameObject);    
    }

}

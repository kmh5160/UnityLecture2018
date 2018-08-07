using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUp : MonoBehaviour {
    public Texture[] cracks;
    public ParticleSystem fx;

    int numHits = 0;
    float lastHitTime;
    float hitTimeThreadhold = 0.2f;

    public void Hit()
    {
        CancelInvoke();
        if (Time.time > lastHitTime + hitTimeThreadhold)
        {
            numHits++;
            
            if(numHits < cracks.Length)
                GetComponent<Renderer>().material.SetTexture("_DetailMask", cracks[numHits]);
            else
            {
                var clone = Instantiate(fx, transform.position, Camera.main.transform.rotation);
                Destroy(clone.gameObject, 2f);
                Destroy(gameObject);
            }
            lastHitTime = Time.time;
        }
        Invoke("Heal", 2f);
        
    }

    void Heal()
    {
        numHits = 0;
        GetComponent<Renderer>().material.SetTexture("_DetailMask", cracks[0]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour {

    public int hp = 10;
    public float moveSpeed = 25f;
    public float maxSpeed = 60f;
    public float rotateSpeed = 10f;
    public float AttackDamage = 5f;

    public Transform playerTransform;
    public LayerMask mask;
    public LayerMask chaseMask;

    public NavMeshAgent agent;
    SpriteRenderer sr;
    Animator anim;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (agent.enabled)
        {
            transform.position = agent.transform.position;
            agent.transform.localPosition = Vector3.zero;
        }
        anim.SetFloat("Distance", Vector3.Distance(transform.position , playerTransform.position));
    }

    Coroutine co;
    public void SetMove(string str)
    {
        if (string.Equals(str, "Start"))
            co = StartCoroutine(Move());
        if (string.Equals(str, "Stop"))
            StopCoroutine(co);
    }

    Vector3 destnation = Vector3.zero;
    Vector3 velo = Vector3.zero;
    IEnumerator Move()
    {
        
        while (true)
        {
            Vector3 target = playerTransform.position;
            //target.y = 0;
            
            RaycastHit hit;
            Vector3 rayDir = target - transform.position;
            rayDir.y = 0;

            rayDir = rayDir.normalized;
            Debug.DrawRay(transform.position, rayDir* 20f, Color.magenta, 1f);
            if (Physics.Raycast(transform.position, rayDir, out hit,20f,chaseMask))
            {
                if (hit.collider.name == "Player")
                {
                    if (agent.enabled)
                    {
                        velo = agent.velocity;
                        agent.transform.localPosition = Vector3.zero;
                        agent.isStopped = true;
                        agent.enabled = false;
                    }
                    destnation = target;
                    velo += rayDir * agent.acceleration * Time.deltaTime;
                    //print(Vector3.Angle(rayDir, rb.velocity));
                    if (velo.magnitude > agent.speed && Vector3.Angle(rayDir, rb.velocity) < 90)        //플레이어를 향해서 올때 속도제한
                        velo = rayDir * agent.speed;

                    //rb.MovePosition(transform.position + velo * Time.deltaTime);
                    //agent.velocity = rayDir.normalized * agent.speed;
                    rb.velocity = velo;
                    yield return null;
                }
                else
                {
                    if (!agent.enabled)
                    {
                        agent.enabled = true;
                        agent.isStopped = false;
                    }
                    agent.destination = destnation;
                    yield return new WaitForSeconds(.5f);
                    if (agent.velocity.magnitude < .5f)
                        anim.SetTrigger("isMissing");
                }
            }
            else
            {
                if (!agent.enabled)
                {
                    agent.enabled = true;
                    agent.isStopped = false;
                }
                agent.destination = destnation;
                yield return new WaitForSeconds(.5f);
                if (agent.velocity.magnitude < .5f)
                    anim.SetTrigger("isMissing");
            }

            Vector3 location = transform.position - target;
            sr.flipX = 0 < location.x ? false : true;
        }
    }

    public void GetDamage(int Damage)
    {
        hp -= Damage;
        if(0 >= hp)
        {
            print(transform.name + " Death!");
            Destroy(gameObject);
        }
    }

    public void KnockBack(Vector3 dir, float power)
    {
        if (!agent.enabled)
        {
            velo = dir.normalized * power;
            rb.velocity = velo;
        }
        else
            agent.velocity = dir.normalized * power;

        //agent.velocity = agent.velocity.normalized * -3f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Aram();
        }
    }

    public void Aram() {

        if (anim.GetBool("isHit"))
            return;

        anim.SetTrigger("isHit");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f/*radius*/, mask);
        if(colliders.Length > 0)
            foreach(var col in colliders)
            {
                //print(col.GetComponentInParent<GameObject>().name);
                col.GetComponentInParent<EnemyControler>().anim.SetTrigger("isHit");
            }
    }

    public void DisableAgent() { agent.enabled = false; rb.velocity = Vector3.zero; }
}

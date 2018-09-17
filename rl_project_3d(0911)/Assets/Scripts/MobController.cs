using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour {
    NavMeshAgent agent;
    public Transform target;
	
	void Start () {

        Chase();

    }
	
	void Update () {
		
	}

    public void Chase()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;

    }
}

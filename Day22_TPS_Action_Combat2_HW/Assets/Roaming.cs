using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roaming : MonoBehaviour {
    public Transform wayPointsRoot;
    public int currentPoint = 0;
    public float moveSpeed = 2;
    public Transform player;

    List<Transform> wayPoints;
    Vector3 nextPoint;

    Animator anim;
    NavMeshAgent agent;

	void Start () {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        wayPoints = new List<Transform>();
        foreach (Transform t in wayPointsRoot)
            wayPoints.Add(t);
        if (currentPoint >= wayPoints.Count)
            currentPoint = 0;
        nextPoint = wayPoints[currentPoint].transform.position;
        // for gizmo
        //for (int i = 0; i < wayPoints.Count; i++)
            //wayPoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
        //wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;

        //anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", true);
    }
	
	void Update () {

        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (direction.magnitude < 5 && angle < 45)
        {
            direction.y = 0;
            anim.SetBool("isIdle", false);
            if(direction.magnitude < 3)
            {
                // Attack
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
            else
            {
                // Chase
                if (!anim.GetBool("isAttacking"))
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Walk"))
                    transform.Translate(0, 0, 0.05f);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
            }
        }
        else
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);

            if (wayPoints.Count >= 2)
            {
                float dist = Vector3.Distance(transform.position, nextPoint);
                if (dist < 0.1f)
                {
                    currentPoint++;
                    if (currentPoint >= wayPoints.Count)
                        currentPoint = 0;
                    nextPoint = wayPoints[currentPoint].transform.position;

                    // for gizmo
                    wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;
                    if (currentPoint == 0)
                    {
                        wayPoints[wayPoints.Count - 1].GetComponent<MeshRenderer>().material.color = Color.magenta;
                    }
                    else
                        wayPoints[currentPoint - 1].GetComponent<MeshRenderer>().material.color = Color.magenta;
                }

                Vector3 dir = nextPoint - transform.position;
                dir.y = 0;
                float distance = dir.magnitude;

                agent.destination = nextPoint;

                // 바라보기
                //transform.LookAt(nextPoint); // 절도있게
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.15f); // 자연스럽게
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.15f);

                // speedAngle = 120;
                // speedAngle * Time.deltaTime / angle;

                // 이동하기
                //transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);
                //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / distance);
                //transform.position = Vector3.Slerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / distance);
            }
        }
		
	}
}

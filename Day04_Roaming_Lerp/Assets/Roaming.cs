using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : MonoBehaviour {
    public Transform wayPointsRoot;
    public int currentPoint = 0;
    public float moveSpeed = 2;
    List<Transform> wayPoints;
    Vector3 nextPoint;

	void Start () {
        wayPoints = new List<Transform>();
        foreach (Transform t in wayPointsRoot)
            wayPoints.Add(t);
        if (currentPoint >= wayPoints.Count)
            currentPoint = 0;
        nextPoint = wayPoints[currentPoint].transform.position;
        // for gizmo
        for (int i = 0; i < wayPoints.Count; i++)
            wayPoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
        wayPoints[currentPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
	
	void Update () {
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

            // 바라보기
            //transform.LookAt(nextPoint); // 절도있게
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.15f); // 자연스럽게
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.15f);

            // speedAngle = 120;
            // speedAngle * Time.deltaTime / angle;

            // 이동하기
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / distance);
            //transform.position = Vector3.Slerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / distance);
        }
	}
}

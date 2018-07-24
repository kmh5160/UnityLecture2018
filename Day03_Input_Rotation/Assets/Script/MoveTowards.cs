using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour {
    public Transform dest;
    public float speed;
    //public GameObject destObject;
    public List<Transform> Waypoints;

    //void Start()
    //{
    //    transform.position = Waypoints[0].position;
    //    Waypoints[0].GetComponent<MeshRenderer>().material.color = Color.magenta;
    //}

    // Update is called once per frame
    void Update () {
        float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, dest.position, step);
        //transform.position = Vector3.MoveTowards(transform.position, destObject.transform.position, 0.1f);
        //transform.position = Vector3.MoveTowards(transform.position, destObject.GetComponent<Transform>().position, 0.1f);

        if (transform.position.magnitude >= Waypoints[0].position.magnitude && transform.position.magnitude < Waypoints[1].position.magnitude)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[1].position, step);
            if (transform.position.magnitude == Waypoints[1].position.magnitude)
            {
                Waypoints[1].GetComponent<MeshRenderer>().material.color = Color.magenta;
                Waypoints[0].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        else if (transform.position.magnitude >= Waypoints[1].position.magnitude && transform.position.magnitude < Waypoints[2].position.magnitude
            && Waypoints[1].GetComponent<MeshRenderer>().material.color == Color.magenta)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[2].position, step);
            if (transform.position.magnitude == Waypoints[2].position.magnitude)
            {
                Waypoints[2].GetComponent<MeshRenderer>().material.color = Color.magenta;
                Waypoints[1].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        else if (transform.position.magnitude > Waypoints[0].position.magnitude && transform.position.magnitude <= Waypoints[2].position.magnitude
            && Waypoints[2].GetComponent<MeshRenderer>().material.color == Color.magenta)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[0].position, step);
            if (transform.position.magnitude == Waypoints[0].position.magnitude)
            {
                Waypoints[0].GetComponent<MeshRenderer>().material.color = Color.magenta;
                Waypoints[2].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[0].position, step);
            if (transform.position.magnitude == Waypoints[0].position.magnitude)
                Waypoints[0].GetComponent<MeshRenderer>().material.color = Color.magenta;
        }
    }
}

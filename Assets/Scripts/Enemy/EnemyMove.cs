using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //[SerializeField]
    //Transform[] waypoints;
    //int currentWaypoint = 0;

    public GameObject player;

    Rigidbody rigidBody;

    [SerializeField]
    float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < .25f)
        //{
        //    currentWaypoint += 1;
        //    currentWaypoint = currentWaypoint % waypoints.Length;
        //}

        //transform.LookAt(player.transform);

        Vector3 dir = (player.transform.position - transform.position).normalized;

        rigidBody.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

    }
}

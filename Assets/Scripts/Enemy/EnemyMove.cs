using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //[SerializeField]
    //Transform[] waypoints;
    //int currentWaypoint = 0;

    public LayerMask enemyLayerMask;

    public GameObject player;

    Rigidbody rigidBody;

    public float sphereRadius = 0.3f;

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
        //Movement();
        Move();
    }

    void Movement()
    {

        Vector3 dir = (player.transform.position - transform.position).normalized;

        this.transform.LookAt(player.transform);

        rigidBody.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

    }

    void Move()
    { 
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        Collider[] collidersHit = Physics.OverlapSphere(this.transform.position, sphereRadius);

        foreach (Collider hit in collidersHit)
        {
            if (hit.gameObject.transform.position != this.transform.position)
            {
                Vector3 dir = (player.transform.position - transform.position).normalized;

                this.transform.LookAt(player.transform);

                rigidBody.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, sphereRadius);
    }
}

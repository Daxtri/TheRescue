using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public LayerMask enemyLayerMask;

    public float attackRadius = 5f;

    public GameObject player;

    Rigidbody rigidBody;

    public float sphereRadius = 0.3f;

    float time_lost = 0;
    bool invoked = true;

    public float attackRate = 80f;

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
        Move();

        if (invoked)
        {
            Attack();
            invoked = false;
        }

        if (time_lost++ == attackRate) //CUSTOM TIMER
        {
            invoked = true;
            time_lost = 0;
        }
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

    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(this.transform.position, attackRadius);

        foreach (Collider c in hitPlayer)
        {
            if (c.tag.Equals("Player"))
            {
                c.gameObject.GetComponent<PlayerController>().TakeDamage(5f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, sphereRadius);
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
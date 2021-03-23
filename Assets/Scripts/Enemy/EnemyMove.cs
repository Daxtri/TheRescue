using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Animator anim;
    public AudioManager audio;
    public LayerMask enemyLayerMask;

    bool sound;

    public float attackRadius = 5f;

    public GameObject player;

    Rigidbody rigidBody;

    public float sphereRadius = 0.3f;

    float time_lost = 0;
    bool invoked = true;

    public float attackRate = 80f;

    public float distance = 20f;

    [SerializeField]
    float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        sound = false;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= distance)
        {
            anim.SetBool("Running", true);
            if (sound == false)
            {
                sound = true;
            }

            Vector3 dir = (player.transform.position - transform.position).normalized;
            rigidBody.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
            rigidBody.rotation = Quaternion.LookRotation(dir);
            this.transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);

            if (dist <= attackRadius)
            {
                if (invoked)
                {
                    anim.SetBool("Running", false);
                    anim.SetBool("Punching", true);
                    Attack();
                    invoked = false;
                }
                else
                {
                    anim.SetBool("Punching", false);
                }

                if (time_lost++ == attackRate) //CUSTOM TIMER
                {
                    invoked = true;
                    time_lost = 0;
                }
            }
        }
        else
        {
            anim.SetBool("Running", false);
            sound = false;
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
        Gizmos.DrawWireSphere(this.transform.position, distance);
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
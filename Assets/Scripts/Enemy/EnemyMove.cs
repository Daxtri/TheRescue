using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Animator anim;
    public AudioManager audio;
    public NavMeshAgent agent;
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
    float moveSpeed;
    float regularSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        regularSpeed = moveSpeed;
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
            agent.isStopped = false;
            anim.SetBool("Running", true);
            if (sound == false)
            {
                sound = true;
            }

            agent.SetDestination(player.transform.position);

            if (dist <= attackRadius)
            {
                agent.isStopped = true;
                if (invoked)
                {
                    anim.SetBool("Running", false);
                    Attack();
                    invoked = false;
                }
                else
                {
                    agent.isStopped = true;
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
            agent.isStopped = true;
            anim.SetBool("Running", false);
            sound = false;
        }
    }

    void Attack()
    {
        anim.SetBool("Punching", true);
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
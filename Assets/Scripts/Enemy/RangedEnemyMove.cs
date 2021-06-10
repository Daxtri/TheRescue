using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyMove : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrel;
    public EnemyHp enemyHp;
    public Animator anim;
    public AudioManager audio;
    NavMeshAgent agent;
    public LayerMask enemyLayerMask, playerMask;

    public float attackRadius = 10f;

    public GameObject player;

    public float sphereRadius = 0.3f;

    float time_lost = 0;
    bool invoked = true;

    public float attackRate = 80f;

    public float distance = 20f;
    //////////////////////////////////
    public bool playerinAttackRange;
    public bool alreadyAttacked;
    public float timeBetweenAttacks, deathTimer = 10f;


    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        enemyHp = GetComponent<EnemyHp>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (enemyHp.dead == false)
            Move();

        else
        {
            agent.isStopped = true;
        }
    }

    void Move()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= distance)
        {
            Debug.Log("in range");
            agent.isStopped = false;
            anim.SetBool("Running", true);

            agent.SetDestination(player.transform.position);

            if (dist <= attackRadius)
            {
               // anim.SetBool("Running", false);
                agent.isStopped = true;
                Attack();
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    public void ChasePlayer()
    {
        agent.isStopped = true;
        anim.SetBool("Running", true);
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        agent.destination = transform.position;
        anim.SetTrigger("Shoot");

        transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, barrel.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 22f, ForceMode.Impulse);
            rb.AddForce(transform.up * 1f, ForceMode.Impulse);

            Collider[] hitPlayer = Physics.OverlapSphere(this.transform.position, attackRadius);
            foreach (Collider c in hitPlayer)
            {
                if (c.tag.Equals("Player"))
                {
                    c.gameObject.GetComponent<PlayerController>().TakeDamage(5f);
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, distance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
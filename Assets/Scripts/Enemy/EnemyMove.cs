using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public EnemyHp enemyHp;
    public Animator anim;
    public AudioManager audio;
    public NavMeshAgent agent;
    public LayerMask enemyLayerMask;

    public float attackRadius = 5f;

    public GameObject player;

    public float sphereRadius = 0.3f;

    float time_lost = 0;
    bool invoked = true;

    public float attackRate = 80f;

    public float distance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        enemyHp = GetComponent<EnemyHp>();
        agent = GetComponent<NavMeshAgent>();

        //agent.speed = Random.Range(2.4f, 8);
        //agent.angularSpeed = 360;
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
                anim.SetBool("Running", false);
                agent.isStopped = true;
                Attack();
            }
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("Running", false);
        }
    }

    void Attack()
    {
        if (invoked)
        {
            agent.isStopped = true;
            anim.SetTrigger("Punching");
            Collider[] hitPlayer = Physics.OverlapSphere(this.transform.position, attackRadius);

            foreach (Collider c in hitPlayer)
            {
                if (c.tag.Equals("Player"))
                {
                    c.gameObject.GetComponent<PlayerController>().TakeDamage(5f);
                }
            }
            invoked = false;
        }

        if (time_lost++ == attackRate) //CUSTOM TIMER
        {
            invoked = true;
            time_lost = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, distance);
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
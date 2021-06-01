using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2Script : MonoBehaviour
{
    GameObject player;
    public GameObject projectile;
    public Transform barrel;
    public HealthBar healthBar;
    public Animator anim;
    NavMeshAgent agent;

    public bool isDead;

    public int maxHealth = 500;
    public int currentHealth;

    public float timeBetweenAttacks, deathTimer = 10f;

    public bool alreadyAttacked;

    public int attackDamage = 20;
    public LayerMask playerMask, groundMask;

    public float detectionRange = 500f, attackRange;

    public bool playerinAttackRange;


    private void Start()
    {
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentHealth <= 0)
            Die();

        if (isDead == false)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
            {
                anim.SetBool("Run", true);
                healthBar.gameObject.SetActive(true);
            }

            playerinAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

            if (!playerinAttackRange) ChasePlayer();
            if (playerinAttackRange) Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        //agent.isStopped = true;
        anim.SetBool("Dead", true);
        isDead = true;
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void ChasePlayer()
    {
        anim.SetBool("Run", true);
        agent.SetDestination(player.transform.position);
    }

    public void Attack()
    {
        agent.destination = transform.position;

        anim.SetTrigger("Shoot");

        transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, barrel.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 22f, ForceMode.Impulse);
            rb.AddForce(transform.up * 1f, ForceMode.Impulse);


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
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

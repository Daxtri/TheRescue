using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    public AudioManager audio;
    public GameObject player;
    public HealthBar healthBar;
    public Animator anim;
    public NavMeshAgent agent;

    public bool isDead;

    public int maxHealth = 500;
    public int currentHealth;

    public float time_lost = 0f, deathTimer = 10f;

    public int attackDamage = 20;
    public Transform attackAnchor;
    public float attackRange = 0.5f;
    public LayerMask attackMask;

    public float detectionRange = 500f;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        attackAnchor = GameObject.FindGameObjectWithTag("AttackAnchor").transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentHealth <= 0)
            Die();


        if (player.GetComponent<PlayerMovement>().isInRangeBoss == true)
        { 
            anim.SetBool("Run", true);
            healthBar.gameObject.SetActive(true);
            Move();
        }
        else
        {
            anim.SetBool("Run", false);
            healthBar.gameObject.SetActive(false);
            agent.SetDestination(transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Move()
    {
        agent.SetDestination(player.transform.position);
    }

    void Die()
    {
        agent.isStopped = true;
        anim.SetBool("Dead", true);
        isDead = true;
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void Attack()
    {
        Vector3 pos = attackAnchor.position;
        Collider[] collInfo = Physics.OverlapSphere(pos, attackRange, attackMask);

        if (collInfo != null)
            foreach (Collider c in collInfo)
                c.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackAnchor.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

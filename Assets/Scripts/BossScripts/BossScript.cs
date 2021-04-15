using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator anim;

    public int maxHealth = 500;
    public int currentHealth;

    public float time_lost = 0f, deathTimer = 10f;

    public int attackDamage = 20;
    public Transform attackAnchor;
    public float attackRange = 0.5f;
    public LayerMask attackMask;

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackAnchor = GameObject.FindGameObjectWithTag("AttackAnchor").transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
            Die();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        anim.SetBool("Dead", true);

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
    }
}

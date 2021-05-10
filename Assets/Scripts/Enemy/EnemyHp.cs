using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public Animator anim;
    public List<GameObject> colliders;
    public float currentHp;
    public float maxHp = 100f;
    public float deathTimer = 1000;
    public bool dead = false;
    
    public float time_lost;

    public GameObject healthPickUp;

    bool instantiated = false;
    void Start()
    {
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        time_lost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        anim.SetBool("Dead", true);

        foreach (GameObject c in colliders) Destroy(c);

        if (instantiated == false)
        {
            Instantiate(healthPickUp, transform.position, Quaternion.identity);
            instantiated = true;
        }
        time_lost++;

        if (time_lost == deathTimer)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }
}

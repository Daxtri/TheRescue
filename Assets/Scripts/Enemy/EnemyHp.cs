using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public Animator anim;
    public float currentHp;
    public float maxHp = 100f;
    public float deathTimer = 1000;
    public bool dead = false;
    [SerializeField]
    float time_lost;

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
        //  slider.value = CalculateHP();

        //if (currentHp < maxHp)
        //   hpUI.SetActive(true);
            
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        dead = true;
        anim.SetBool("Dead", true);

        if (instantiated == false)
        {
            Instantiate(healthPickUp);
            healthPickUp.transform.position = this.transform.position;
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

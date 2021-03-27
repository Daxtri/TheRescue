using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public Animator anim;
    public float currentHp;
    public float maxHp = 100f;
    public bool dead = false;

    void Start()
    {
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
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
        anim.SetBool("Dead", true);
        dead = true;
    }

    float CalculateHP()
    {
        return currentHp / maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }

}

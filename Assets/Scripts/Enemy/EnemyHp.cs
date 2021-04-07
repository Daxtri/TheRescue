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
    [SerializeField]
    float time_lost;

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
        anim.SetBool("Dead", true);
        dead = true;

        if (dead)
        {
            time_lost++;

                if(time_lost == 3000)
                {
                    Destroy(gameObject);
                }

        }
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

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

   /* public void DropItem()
    {
        //RANDOMIZE
        randNum = Random.Range(0, 101); // 100% total for determining loot chance;
        //INSTACIAR OBJETO
        if (randNum <= 75)
        {
            itemNum = 1;
            Instantiate(itemList[itemNum], NPCpos.position, Quaternion.identity);
        }
        else if (randNum > 75 && randNum < 95)
        {
            itemNum = 0;
            Instantiate(itemList[itemNum], NPCpos.position, Quaternion.identity);
        }
        else if (randNum >= 95)
        {
            itemNum = 0;
            itemNum2 = 1;
            Instantiate(itemList[itemNum], NPCpos.position, Quaternion.identity);
            Instantiate(itemList[itemNum2], NPCpos.position, Quaternion.identity);
        }
    }*/
}

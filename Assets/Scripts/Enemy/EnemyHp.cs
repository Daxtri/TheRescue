using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    System.Random random;
    public CharacterController player;
    public Animator anim;
    public List<GameObject> colliders;
    public GameObject health, armor, rifleAmmo, sniperAmmo;
    public float currentHp;
    public float maxHp = 100f;
    public float deathTimer = 1000;
    public bool dead = false;
    
    public float time_lost;

    public GameObject[] pickups;

    bool instantiated = false;
    void Start()
    { 
        currentHp = maxHp;
        anim = GetComponentInChildren<Animator>();
        time_lost = 0;
        pickups = new GameObject[4];
        pickups[0] = health;
        pickups[1] = armor;
        pickups[2] = rifleAmmo;
        pickups[3] = sniperAmmo;
        random = new System.Random();
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
            Instantiate(pickups[random.Next(0, 3)], transform.position, Quaternion.identity);
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

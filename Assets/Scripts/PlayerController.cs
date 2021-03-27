using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float currentHealth;
    [SerializeField]
    public float armor = 140f;
    [SerializeField]
    public float jumpForce = 1.5f;
    [SerializeField]
    public float gravity = -40f;
    [SerializeField]
    public float sprintSpeed = 10.0f;
    [SerializeField]
    public float crouchSpeed = 4.0f;
    [SerializeField]
    public float normalSpeed = 7.0f;
    [SerializeField]
    public float originalHeight;
    [SerializeField]
    public float crouchHeight;

    void Awake()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        originalHeight = GetComponent<CharacterController>().height;
        crouchHeight = originalHeight/2;
    }
    void Update()
    {
        if(currentHealth<=0)
        Die();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    void Die()
    {
        //Destroy(this.gameObject);
        Debug.Log("Dead");
    }
}
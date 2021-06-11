using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float currentHealth;
    [SerializeField]
    public float maxArmor;
    [SerializeField]
    public float currentArmor;
    [SerializeField]
    public float jumpForce = 1.5f;
    [SerializeField]
    public float gravity = -40f;
    [SerializeField]
    public float sprintSpeed = 15.0f;
    [SerializeField]
    public float crouchSpeed = 4.0f;
    [SerializeField]
    public float normalSpeed = 7.0f;
    [SerializeField]
    public float originalHeight;
    [SerializeField]
    public float crouchHeight;

    public bool isReloading;

    void Awake()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        maxArmor = 140f;
        currentArmor = maxArmor;
        originalHeight = GetComponent<CharacterController>().height;
        crouchHeight = originalHeight/2;
        isReloading = false;
    }
    void Update()
    {
        if (currentHealth <= 0)
            Die();

        #region Health and Armor Caps
        if (currentArmor <= 0)
            currentArmor = 0;

        if (currentArmor >= maxArmor)
            currentArmor = maxArmor;

        if (currentHealth <= 0)
            currentHealth = 0;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        #endregion
    }

    public void TakeDamage(float damage)
    {
        if (currentArmor >= damage)
            currentArmor -= damage;

        else
        {
            float diff = damage - currentArmor;
            currentArmor = 0;
            currentHealth -= diff;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("BossBullet"))
            TakeDamage(20f);

        if (other.tag.Equals("Final"))
            SceneManager.LoadScene("VictoryScene");
    }

    void Die()
    {
        SceneManager.LoadScene("DeathScene");
        Debug.Log("Dead");
    }
}
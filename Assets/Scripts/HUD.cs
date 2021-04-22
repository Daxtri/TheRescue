using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public PlayerController playerController;

    public Slider hpBar, armorBar;
    public Text counter; //ammostowed1, ammostowed2

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }


    void Update()
    {
        //HEALTH ARMOR
        hpBar.value = playerController.currentHealth;
        hpBar.maxValue = playerController.maxHealth;
        armorBar.value = playerController.currentArmor;

        if (playerController.currentArmor <= 0) armorBar.fillRect.gameObject.SetActive(false);
        if (playerController.currentHealth <= 0) hpBar.fillRect.gameObject.SetActive(false);

        //COUNTER
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberEnemy = enemies.Length;
        counter.text = "Enemies Remaining: " + numberEnemy.ToString();

        if (numberEnemy <= 0) counter.enabled = false;
    }
}

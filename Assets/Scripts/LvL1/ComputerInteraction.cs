using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    BossScript boss;
    public bool canCompute;
    public bool canActivate;

    void Start()
    {
        canActivate = false;
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
    }

    void Update()
    {
        if (boss.isDead == true)
            canCompute = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canCompute)
        {
            canActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canActivate = false;
    }
}

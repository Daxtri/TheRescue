using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public Animator anim;
    BossScript boss;

    void Start()
    {
        anim = GetComponent<Animator>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.isDead == true)
            anim.SetBool("Open", true);
    }
}

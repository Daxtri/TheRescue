using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject computer;
    public Animator anim;
    int time_lose = 0;
    public bool finishedLvL;


    void Start()
    {
        finishedLvL = false;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (computer.GetComponent<ComputerInteraction>().canActivate && Input.GetKey(KeyCode.E))
        {
            if (time_lose++ == 5000) //CUSTOM TIMER
            {
                time_lose = 0;
                startAnim();
            }
        }
    }



    private void startAnim()
    {
        anim.SetBool("Open", true);
        finishedLvL = true;
    }
}

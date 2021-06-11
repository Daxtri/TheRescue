using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Open : MonoBehaviour
{
    public GameObject computer;
    public Animator anim;
    public int time_lose = 0;
    public bool finishedLvL;
    public Slider load;


    void Start()
    {
        finishedLvL = false;
        anim = GetComponent<Animator>();
        load.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (computer.GetComponent<ComputerInteraction>().canActivate && Input.GetKey(KeyCode.E))
        {
            load.gameObject.SetActive(true);
            load.value = time_lose / 50;
            if (time_lose++ == 100) //CUSTOM TIMER
            {
                time_lose = 0;
                startAnim();
            }

            if (finishedLvL == true) load.gameObject.SetActive(false);
        }
    }

    private void startAnim()
    {
        anim.SetBool("Open", true);
        finishedLvL = true;
    }
}

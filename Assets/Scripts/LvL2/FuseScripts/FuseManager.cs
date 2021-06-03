using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseManager : MonoBehaviour
{
    public GameObject blue, red, orange;
    public Animator door;
    public GameObject boss;
    public bool openned;
    void Start()
    {
        openned = false;
    }

    void Update()
    {
        if (boss.GetComponent<Boss2Script>().isDead == true)
        {
            if (blue.GetComponent<FuseScript>().activated == true)
            {
                if (red.GetComponent<FuseScript>().activated == true)
                {
                    if (orange.GetComponent<FuseScript>().activated == true)
                    {
                        door.SetBool("Open", true);
                        openned = true;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Animator doorAnim;
    public bool activated = false;
    public bool open = false;

    void Update()
    {
        if(activated == true)
        {
            open = !open;

            if (open == false)
                doorAnim.SetTrigger("Close");
            else if (open == true)
                doorAnim.SetTrigger("Open");

            activated = false;
        }
    }
}

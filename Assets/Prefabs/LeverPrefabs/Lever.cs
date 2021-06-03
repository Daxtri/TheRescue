using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator door, lever;
    public bool activated;
    private void Start()
    {
        activated = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            activated = !activated;

            if(activated)
            {
                StartCoroutine(SetLever1());             
            }
            else
            {
                StartCoroutine(SetLever2());
            }
        }
    }

    IEnumerator SetLever1()
    {
        lever.SetBool("Activate", true);
        yield return new WaitForSeconds(1);
        door.SetBool("Open", true);
    }

    IEnumerator SetLever2()
    {
        lever.SetBool("Activate", false);
        yield return new WaitForSeconds(1);
        door.SetBool("Open", false);
    }
}

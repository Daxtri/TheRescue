using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseManager : MonoBehaviour
{
    public GameObject blue, red, orange;
    public Animator door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(blue.GetComponent<FuseScript>().activated == true)
        {
            if(red.GetComponent<FuseScript>().activated == true)
            {
                if (orange.GetComponent<FuseScript>().activated == true)
                {
                    door.SetBool("Open", true);
                }
            }
        }
    }
}

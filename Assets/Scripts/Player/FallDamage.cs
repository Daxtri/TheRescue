using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public Vector3 enterPos;
    public Vector3 exitPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            enterPos = transform.position;

            if(exitPos.y - enterPos.y > 2f)
            {
                this.GetComponent<PlayerController>().currentHealth -= 2 * exitPos.y - enterPos.y;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            exitPos = transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public int targetHealth;

    // Update is called once per frame
    void Update()
    {
        if (targetHealth <= 0)
            Destroy(this.gameObject);
    }
}
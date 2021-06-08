using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public int targetHealth;
    // Start is called before the first frame update
    void Start()
    {
        targetHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetHealth <= 0)
            Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject enemy;
    bool pos = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject enemy1 = Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform barrel;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, barrel);

            if (newBullet) Debug.Log("instanciada");

            newBullet.transform.position += barrel.forward;
            
        }
    }
}

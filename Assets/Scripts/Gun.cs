using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 20f;
    public float range = 200f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position,
            fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 2f);
    }
}
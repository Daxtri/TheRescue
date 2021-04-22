using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
{
    public bool isActive;
    public AudioManager audio;
    public Animator anim;
    public float damage = 20f;
    public float range = 80f;
    public float fireRate = 15f;

    private float nextShot = 2f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public LineRenderer bulletTrail;
    public Transform shootPoint;

    public Text ammo, ammoReserves;

    public float currentAmmo, maxAmmo = 30f;

    private void Start()
    {
        isActive = false;
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        UpdateHud();

        if (Input.GetButtonDown("Fire1") && Time.time >= nextShot)
        {
            nextShot = Time.time + 0.5f / fireRate;
            if (currentAmmo > 0)
                Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            currentAmmo = maxAmmo;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            string tag = hit.transform.tag;

            switch (tag)
            {
                case "Enemy":
                    EnemyHp enemy = hit.transform.GetComponent<EnemyHp>();
                    enemy.TakeDamage(damage);
                    break;

                case "Vent":
                    Destroy(hit.transform.gameObject);
                    audio.Play("Vent");
                    break;

                case "Boss":
                    BossScript boss = hit.transform.GetComponent<BossScript>();
                    boss.TakeDamage((int)damage);
                    break;
            }

            SpawnBulletTrail(hit.point);
        }

        anim.SetTrigger("Shoot");
        audio.Play("Gun Shot");

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);
        currentAmmo--;
    }

    private void SpawnBulletTrail(Vector3 hitPoint)
    {
        GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, shootPoint.position, Quaternion.identity);

        LineRenderer lR = bulletTrailEffect.GetComponent<LineRenderer>();
        lR.SetPosition(0, shootPoint.position);
        lR.SetPosition(1, hitPoint);

        Destroy(bulletTrailEffect, 0.5f);
    }

    void UpdateHud()
    {
        ammo.text = currentAmmo.ToString();
        ammoReserves.text = "/" + maxAmmo.ToString();

        if (currentAmmo <= 5) ammo.color = new Color(1, 0, 0, 1);
        else ammo.color = new Color(1, 1, 0, 1);
    }
}
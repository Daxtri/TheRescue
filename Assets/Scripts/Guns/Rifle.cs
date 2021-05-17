using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rifle : MonoBehaviour
{
    public AudioManager audio;
    public Animator anim;
    public float damage = 20f;
    public float range = 80f;
    public float fireRate = 15f;

    public Recoil recoil;
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bulletHole;
    public LineRenderer bulletTrail;
    public Transform shootPoint;

    public float headshotDamage, armDamage, legDamage, bodyDamage;

    public float nextShot = 0f;

    public Text ammo, ammoReserves;

    public float currentAmmo, maxAmmo = 30f;

    private void Start()
    {
        headshotDamage = damage * 5f;
        armDamage = damage / 2f;
        legDamage = damage / 1.5f;
        bodyDamage = damage;
        recoil = GetComponent<Recoil>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    void Update()
    { 
        if (Input.GetButton("Fire1") && Time.time >= nextShot)
        {
            nextShot = Time.time + 1f/ fireRate;
            if (currentAmmo > 0)
                Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
            currentAmmo = maxAmmo;
        }

        UpdateHud();
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
                case "Head":
                    hit.transform.gameObject.GetComponentInParent<EnemyHp>().TakeDamage(headshotDamage);
                    break;
                case "Arms":
                    hit.transform.gameObject.GetComponentInParent<EnemyHp>().TakeDamage(armDamage);
                    break;
                case "Legs":
                    hit.transform.gameObject.GetComponentInParent<EnemyHp>().TakeDamage(legDamage);
                    break;
                case "Body":
                    hit.transform.gameObject.GetComponentInParent<EnemyHp>().TakeDamage(bodyDamage);
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
        else
        {
            recoil.Fire();

            anim.SetTrigger("Shoot");
            audio.Play("Gun Shot");

            currentAmmo--;
            return;
        }

        recoil.Fire();

        anim.SetTrigger("Shoot");
        audio.Play("Gun Shot");

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        if (!hit.transform.tag.Equals("Legs") && !hit.transform.tag.Equals("Arms") && !hit.transform.tag.Equals("Head") && !hit.transform.tag.Equals("Body")
           && !hit.transform.tag.Equals("Enemy") && !hit.transform.tag.Equals("Boss") && !hit.transform.tag.Equals("Vent"))
        {
            GameObject bulletHol3 = Instantiate(bulletHole, hit.point + hit.normal * 0.0001f, Quaternion.identity) as GameObject;
            bulletHol3.transform.LookAt(hit.point + hit.normal);
            Destroy(bulletHol3, 5f);
        }

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
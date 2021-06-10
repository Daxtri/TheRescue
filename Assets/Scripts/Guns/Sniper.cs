using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sniper : MonoBehaviour
{
    public GameObject sniperScope;
    public AudioManager audio;
    public Animator anim;
    public float damage = 100f;
    public float range = 80f;
    public float fireRate = 15f;
    public bool isReloading;
    public float nextShot = 5f;
    public Camera fpsCamera, weaponCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bulletHole;
    public LineRenderer bulletTrail;
    public Transform shootPoint;
    public float headshotDamage, armDamage, legDamage, bodyDamage;
    public float mouseSens;
    public Text ammo, ammoReserves;
    public bool scoped;
    public int currentAmmo, maxAmmo = 1, curReserve, maxReserve = 30;

    private void Start()
    {
        scoped = false;
        mouseSens = fpsCamera.GetComponent<FpsCamera>().mouseSensitivity;
        isReloading = false;
        headshotDamage = damage * 3f;
        armDamage = damage;
        legDamage = damage / 1.2f;
        bodyDamage = damage;
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        UpdateHud();
        if (scoped == false && Input.GetMouseButtonDown(1))
        {
            Scope();
        }
        else if (scoped == true && Input.GetMouseButtonDown(1))
        {
            Unscope();
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextShot)
        {
            nextShot = Time.time + .5f / fireRate;
            if (currentAmmo > 0)
                Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 1 && curReserve > 0)
        {
            anim.SetTrigger("Reload");
            Unscope();
            isReloading = true;
            GetComponentInParent<PlayerController>().isReloading = true;
        }
    }

    void Reload()
    {
        if (curReserve > 0)
        {
            currentAmmo = maxAmmo;
            curReserve--;
        }
    }

    void Shoot()
    {
        Unscope();
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
                case "Boss2":
                    Boss2Script boss2 = hit.transform.GetComponent<Boss2Script>();
                    boss2.TakeDamage((int)damage);
                    break;

                case "FuseBox":
                    hit.transform.gameObject.GetComponent<FuseScript>().activated = true;
                    break;

                case null:
                    anim.SetTrigger("Shoot");
                    audio.Play("Gun Shot");
                    break;
            }

            SpawnBulletTrail(hit.point);
        }

        anim.SetTrigger("Shoot");
        audio.Play("Gun Shot");

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        if (!hit.transform.tag.Equals("Legs") && !hit.transform.tag.Equals("Arms") && !hit.transform.tag.Equals("Head") && !hit.transform.tag.Equals("Body")
           && !hit.transform.tag.Equals("Enemy") && !hit.transform.tag.Equals("Boss") && !hit.transform.tag.Equals("Boss2") && !hit.transform.tag.Equals("Vent"))
        {
            GameObject bulletHol3 = Instantiate(bulletHole, hit.point + hit.normal * 0.0001f, Quaternion.identity) as GameObject;
            bulletHol3.transform.LookAt(hit.point + hit.normal);
            Destroy(bulletHol3, 5f);
        }

        Destroy(impact, 1f);
        currentAmmo--;
    }

    void Scope()
    {
        fpsCamera.GetComponent<Camera>().fieldOfView = 10;
        fpsCamera.GetComponent<FpsCamera>().mouseSensitivity /= 3f;
        weaponCamera.enabled = false;
        sniperScope.SetActive(true);
        scoped = true;
    }
    public void Unscope()
    {
        fpsCamera.GetComponent<Camera>().fieldOfView = 60;
        fpsCamera.GetComponent<FpsCamera>().mouseSensitivity = mouseSens;
        weaponCamera.enabled = true;
        sniperScope.SetActive(false);
        scoped = false;
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
        ammoReserves.text = "/" + curReserve.ToString();

        if (currentAmmo <= 5) ammo.color = new Color(1, 0, 0, 1);
        else ammo.color = new Color(1, 1, 0, 1);
    }
}
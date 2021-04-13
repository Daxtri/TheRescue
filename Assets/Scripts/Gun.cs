using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public AudioManager audio;
    public Animator anim;
    public float damage = 20f;
    public float range = 200f;
    public float fireRate = 15f;

    private float nextShot = 0f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public LineRenderer bulletTrail;
    public Transform shootPoint;

    public Text text;

    public float currentAmmo, maxAmmo = 30f;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextShot)
        {
            if (this.tag == "Pistol")
            {
                nextShot = Time.time + 0.5f / fireRate;
                if (currentAmmo > 0)
                    Shoot();
                else
                {

                }
            }

            if (this.tag == "Rifle")
            {
                damage = 33f;
                nextShot = Time.time + 0.5f / fireRate;
                if (currentAmmo > 0)
                    Shoot();
                else
                {

                }
            }

            if (this.tag == "Sniper")
            {
                damage = 100;
                nextShot = Time.time + 0.5f / fireRate;
                if (currentAmmo > 0)
                    Shoot();
                else
                {

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            //maxAmmo = 30 - (30 - currentAmmo);
            anim.SetTrigger("Reload");
            currentAmmo = maxAmmo;
        }
        text.text = currentAmmo.ToString();
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Enemy")
            {
                EnemyHp enemy = hit.transform.GetComponent<EnemyHp>();
                enemy.TakeDamage(damage);
            }
            //Debug.Log(hit.transform.name);
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
}
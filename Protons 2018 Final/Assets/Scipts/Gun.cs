﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    public int maxAmmo = 100;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public ParticleSystem particleSystem;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Camera fpsCam;
    public GameObject impactEffect;

    private void Start()
    {
        currentAmmo = 10;
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
        {
            return;
        }

        if ((currentAmmo <= 0 && maxAmmo >0)||Input.GetButton("Reload"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo>0)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading..");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);
		maxAmmo = maxAmmo-10+currentAmmo;
        currentAmmo = 10;		
        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        if (Input.GetButton("Fire1") && !isReloading) 
        {
            particleSystem.Play();
            audioSource.PlayOneShot(audioClip);
        }
        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * 30f);
        }
        GameObject hitGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitGo, 2f);
    }
}
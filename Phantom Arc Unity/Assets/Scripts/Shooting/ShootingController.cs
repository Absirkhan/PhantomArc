using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform firepoint;
    public float fireRate = 0.1f;
    public float fireRange = 10f;
    private float nextFireTime = 0f;
    public bool isAuto = false;
    public int maxammo = 30;
    public int currentammo;
    private float reloadtime = 1.5f;
    private bool isReloading = false;

    private void Start()
    {
        currentammo = maxammo;
    }

    void Update()
    {
        if (isReloading)
            return;
            
        
        if (isAuto == true)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && currentammo < maxammo)
        {
            Reload();
        }
    }


    private void Shoot()
    {
        if (currentammo > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(firepoint.position, firepoint.forward, out hit, fireRange))
            {
                Debug.Log(hit.transform.name);
            }

            currentammo--;
        }
        else
        {
            Reload();
        }
    }

    private void Reload()
    {
        if (!isReloading && currentammo < maxammo)
        {
            isReloading = true;
            Invoke("FinishReloading", reloadtime);
        }

    }

    private void FinishReloading()
    {
        currentammo = maxammo;
        isReloading = false;

    }
}

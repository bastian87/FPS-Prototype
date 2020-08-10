using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] Camera fpsCamera;
    [SerializeField] float shootingRange = 10f;
    [SerializeField] float damage = 30f;

    [Header("Particles")]
    [SerializeField] ParticleSystem muzzleFlash;

   

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRayMethod();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRayMethod()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, shootingRange))
        {
            // This decrease the enemy health.
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }
}

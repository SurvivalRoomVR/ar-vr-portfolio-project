using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScriptNotVR : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    private float fireRate = 3f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public Transform shootingPoint;
    private float nextTimeToFire = 0f;
    public Pool fleshBulletPool;
    public Pool GenericBulleHittPool;
    // Start is called before the first frame update
    void Start()
    {
        fpsCam = Camera.main;
        // shootingPoint = fpsCam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Stop();
        muzzleFlash.Play();
        audioSource.PlayOneShot(fire);
        // animator.SetTrigger("Fire");
        animator.CrossFadeInFixedTime("Fire", 0.1f);
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
        {
            EnemyHealth health = hit.transform.GetComponent<EnemyHealth>();
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            if (health != null)
            {
                // Instantiate(fleshBulletHole, hit.point, Quaternion.LookRotation(hit.normal), health.gameObject.transform);
                // Instantiate(fleshBulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                // fleshBulletPool.ActivateNext(hit.point, Quaternion.LookRotation(hit.normal));
                // health.TakeDamage(damage);

                health.TakeDamage(damage, hit);
            }
            else
            {
                // GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                // Destroy(impactGO, 1f);
                GenericBulleHittPool.ActivateNext(hit.point, Quaternion.LookRotation(hit.normal));
                // audioSource.PlayOneShot(genericHit);
            }
        }
    }

    public AudioSource audioSource;
    public AudioClip fire;
    public AudioClip genericHit;
}

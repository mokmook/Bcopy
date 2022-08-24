using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun cur_Gun;

    private float cur_FireRate;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        GunFireRateCalc();
        TryFire();
    }
    private void GunFireRateCalc()
    {
        if (cur_FireRate>0)
        {
            cur_FireRate -= Time.deltaTime;
        }
    }
    private void TryFire()
    {
        if (Input.GetButton("Fire1")&&cur_FireRate<=0) 
        {
            cur_FireRate = cur_Gun.fireRate;
            Shoot();
        }
    }
    private void Shoot()
    {
        PlaySE(cur_Gun.fire_Sound);
        cur_Gun.muzzleFlash.Play();
        print("dsa");
    }
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}

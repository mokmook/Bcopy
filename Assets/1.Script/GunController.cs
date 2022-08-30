using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun cur_Gun;

    private float cur_FireRate;
    private bool isReload;
    private bool isFindSightMode = false;

    [SerializeField] Vector3 orignPos;
    AudioSource audioSource;

    Vector3 recoilBack;
    Vector3 retroActionRecoilBack;

    private RaycastHit hit;
    [SerializeField]private Camera theCam;
    [SerializeField] private GameObject hitEffectPrefab;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        recoilBack = new Vector3(cur_Gun.retroActionForce, orignPos.y, orignPos.z);
        retroActionRecoilBack = new Vector3(cur_Gun.retroActionFineSightForce, cur_Gun.fineSightOriginPos.y, cur_Gun.fineSightOriginPos.z);
    }
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFindSight();
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
        if (Input.GetButton("Fire1") && cur_FireRate <= 0 &&!isReload )
        {
            Fire();
        }
       
    }
    private void Fire()
    {
        if (!isReload)
        {
            if (cur_Gun.cur_BulletCount > 0)
                Shoot();
            else
            {
                CancelFindSight();
                StartCoroutine("ReloadCoroutine");
            }
        }
    }
    private void Shoot()
    {
        cur_Gun.cur_BulletCount--;
        cur_FireRate = cur_Gun.fireRate;
        PlaySE(cur_Gun.fire_Sound);
        cur_Gun.muzzleFlash.Play();
        Hit();
        StopAllCoroutines();
        StartCoroutine("RetroActionCoroutine");

    }
    private void Hit()
    {
        if(Physics.Raycast(theCam.transform.position,theCam.transform.forward,out hit, cur_Gun.range))
        {
            GameObject clone=Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(clone, 2f);
        }
    }
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R)&&!isReload&&cur_Gun.cur_BulletCount<cur_Gun.reloadBulletCount)
        {
            CancelFindSight();
            StartCoroutine("ReloadCoroutine");
        }
    }
    private void TryFindSight()
    {
        if (Input.GetButtonDown("Fire2")&&!isReload)
        {
            FindSight();
        }
    }
    public void CancelFindSight()
    {
        if (isFindSightMode)
            FindSight();
    }
    private void FindSight()
    {
        isFindSightMode = !isFindSightMode;
        cur_Gun.anim.SetBool("FindSightMode", isFindSightMode);
        if (isFindSightMode)
        {
            StopAllCoroutines();
            StartCoroutine("FindSightActiveCoroutine");
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine("FindSightDeactiveCoroutine");
        }
    }
    IEnumerator FindSightActiveCoroutine()
    {
        while (cur_Gun.transform.localPosition != cur_Gun.fineSightOriginPos)
        {
            cur_Gun.transform.localPosition = Vector3.Lerp(cur_Gun.transform.localPosition, cur_Gun.fineSightOriginPos, .2f);
            yield return null;
        }
    }
    IEnumerator FindSightDeactiveCoroutine()
    {
        while (cur_Gun.transform.localPosition != orignPos)
        {
            cur_Gun.transform.localPosition = Vector3.Lerp(cur_Gun.transform.localPosition, orignPos, .2f);
            yield return null;
        }
    }
    IEnumerator ReloadCoroutine()
    {
        if (cur_Gun.carryBulletCount > 0)
        {
            isReload = true;
            cur_Gun.anim.SetTrigger("Reload");
            cur_Gun.carryBulletCount += cur_Gun.cur_BulletCount;
            cur_Gun.cur_BulletCount = 0;
            yield return new WaitForSeconds(cur_Gun.reloadTime);
            if (cur_Gun.carryBulletCount >= cur_Gun.cur_BulletCount)
            {
                cur_Gun.cur_BulletCount = cur_Gun.reloadBulletCount;
                cur_Gun.carryBulletCount -= cur_Gun.reloadBulletCount;
            }
            else
            {
                cur_Gun.cur_BulletCount = cur_Gun.carryBulletCount;
                cur_Gun.carryBulletCount = 0;
            }

            isReload = false;
        }
        else
            Debug.Log("소유한 총알이 없습니다");
    }
    IEnumerator RetroActionCoroutine()
    {
        

        if (!isFindSightMode)
        {
            cur_Gun.transform.localPosition = orignPos;
            //반동시작
            while (cur_Gun.transform.localPosition.x <= cur_Gun.retroActionForce-0.02f)
            {
                cur_Gun.transform.localPosition = Vector3.Lerp(cur_Gun.transform.localPosition, recoilBack, .4f);
                yield return null;
            }
            //원위치
            while (cur_Gun.transform.localPosition!=orignPos)
            {
                cur_Gun.transform.localPosition = Vector3.Lerp(cur_Gun.transform.localPosition, orignPos, .1f);
                yield return null;
            }
        }
        else
        {
            cur_Gun.transform.localPosition = cur_Gun.fineSightOriginPos;
            //반동시작
            while (cur_Gun.transform.localPosition.x <= cur_Gun.retroActionFineSightForce - 0.02f)
            {
                cur_Gun.transform.localPosition = Vector3.Lerp(cur_Gun.transform.localPosition, retroActionRecoilBack, .4f);
                yield return null;
            }
            //원위치
            while (cur_Gun.transform.localPosition != cur_Gun.fineSightOriginPos)
            {
                cur_Gun.transform.localPosition = Vector3.Lerp(cur_Gun.transform.localPosition,cur_Gun.fineSightOriginPos, .1f);
                yield return null;
            }
        }
            
    }
    public Gun GetGun()
    {
        return cur_Gun;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string handName;
    public float range;
    public float accuracy; //정확도
    public float fireRate;
    public float reloadTime;
    public int damage;

    public int reloadBulletCount;
    public int cur_BulletCount;
    public int maxBulletCount;
    public int carryBulletCount;

    public float retroActionForce;//반동
    public float retroActionFineSightForce; //정조준 반동

    public Vector3 fineSightOriginPos;

    public Animator anim;

    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;
}

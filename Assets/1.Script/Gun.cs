using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string handName;
    public float range;
    public float accuracy; //��Ȯ��
    public float fireRate;
    public float reloadTime;
    public int damage;

    public int reloadBulletCount;
    public int cur_BulletCount;
    public int maxBulletCount;
    public int carryBulletCount;

    public float retroActionForce;//�ݵ�
    public float retroActionFineSightForce; //������ �ݵ�

    public Vector3 fineSightOriginPos;

    public Animator anim;

    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;
}

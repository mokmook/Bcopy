using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private GunController theGunController;
    private Gun currentGun;

    [SerializeField] private GameObject goBulletHUD;

    [SerializeField] private TextMeshProUGUI[] text_Bullet;

    void Update()
    {
        CheckBullet();
    }
    private void CheckBullet()
    {
        currentGun=theGunController.GetGun();
        text_Bullet[0].text=currentGun.carryBulletCount.ToString();
        text_Bullet[1].text=currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.cur_BulletCount.ToString();
    }
}

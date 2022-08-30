using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Animator anim;

    //크로스헤어에 따른 정확도
    float gunAccuracy;

    [SerializeField] GameObject goCrosshairHUD;
    [SerializeField] GunController theGunController;


    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.cur_WeaponAnim.SetBool("Walk", _flag);
        anim.SetBool("Walking", _flag);
    }
    public void RunningAnimation(bool _flag)
    {
        WeaponManager.cur_WeaponAnim.SetBool("Run", _flag);
        anim.SetBool("Running", _flag);
    }
    public void CrouchingAnimation(bool _flag)
    {
        anim.SetBool("Crouching", _flag);
    }
    public void FineSightAnimation(bool _flag)
    {
        anim.SetBool("FindSight", _flag);
    }
    public void FireAnimation()
    {
        if (anim.GetBool("Walking"))
            anim.SetTrigger("Walk_Fire");
        else if (anim.GetBool("Crouching"))
            anim.SetTrigger("Crouch_Fire");
        else
            anim.SetTrigger("Idle_Fire");
    }
    public float GetAccuracy()
    {
        gunAccuracy = anim.GetBool("Walking") ? 0.08f : 
            anim.GetBool("Crouching") ? 0.02f : 
            theGunController.GetFineSight() ? 0.001f : 0.04f;

        return gunAccuracy;
    }

}

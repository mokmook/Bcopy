using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public static bool isActivate = false;


    [SerializeField] private Arm cur_Arm;
    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hit;
    private void Update()
    {
        if(isActivate)
        TryAttack();
    }
    private void TryAttack()
    {
        if (Input.GetButton("Fire1")&&!isAttack) 
        {          
            StartCoroutine("AttackCoroutine");
        }
    }
    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        cur_Arm.anim.SetTrigger("Attack");
        yield return new WaitForSeconds(cur_Arm.attackDelayA);
        isSwing = true;
        StartCoroutine(HitCoroutine());
        yield return new WaitForSeconds(cur_Arm.attackDelayB);
        isSwing = false;
        yield return new WaitForSeconds(cur_Arm.attackDelay- cur_Arm.attackDelayA- cur_Arm.attackDelayB);
        isAttack = false;
    }
    IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObj())
            {
                Debug.Log(hit.transform.name);
                isSwing =false;             
            }
            yield return null;
        }
    }
    private bool CheckObj()
    {
        if(Physics.Raycast(transform.position,transform.forward,out hit, cur_Arm.range))
        {
            return true;
        }                                
        return false;
    }
    public void ArmChange(Arm _arm)
    {
        if (WeaponManager.cur_Weapon != null)
        {
            WeaponManager.cur_Weapon.gameObject.SetActive(false);
        }
        cur_Arm = _arm;
        WeaponManager.cur_Weapon = cur_Arm.GetComponent<Transform>();
        WeaponManager.cur_WeaponAnim = cur_Arm.anim;

        cur_Arm.transform.localPosition = Vector3.zero;
        cur_Arm.gameObject.SetActive(true);
    }
}

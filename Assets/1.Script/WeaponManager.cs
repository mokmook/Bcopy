using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon=false;
    [SerializeField] private float changeWeaponDelayTime;
    [SerializeField] private float changeWeaponEndDelayTime;

    [SerializeField] private Gun[] guns;
    [SerializeField] private Arm[] arms;

    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, Arm> armDictionary = new Dictionary<string, Arm>();

    [SerializeField] private GunController theGunController;
    [SerializeField] private ArmController theArmController;

    [SerializeField] private string cur_WeaponType;

    public static Transform cur_Weapon;
    public static Animator cur_WeaponAnim;

    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < arms.Length; i++)
        {
            armDictionary.Add(arms[i].handName, arms[i]);

        }
    }

    void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                StartCoroutine(ChangeWeaponCoroutine("HAND", "맨손"));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                StartCoroutine(ChangeWeaponCoroutine("GUN", "머신건1"));
        }
    }
    public IEnumerator ChangeWeaponCoroutine(string _type,string _name)
    {
        isChangeWeapon = true;
        cur_WeaponAnim.SetTrigger("WeaponOut");
        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type,_name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        cur_WeaponType = _type;
        isChangeWeapon = false;
    }
    private void CancelPreWeaponAction()
    {
        switch (cur_WeaponType)
        {
            case "GUN":
                theGunController.CancelFindSight();
                theGunController.CancelReload();
                GunController.isActivate = false;
                break;
            case "HAND":
                ArmController.isActivate = false;
                break;
                
        }
    }
    private void WeaponChange(string _type, string _name)
    {
        if (_type=="GUN")        
            theGunController.GunChange(gunDictionary[_name]);

        else if (_type == "HAND")
            theArmController.ArmChange(armDictionary[_name]);
    }
}

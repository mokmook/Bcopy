using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private Arm cur_Arm;

    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hit;
    void Start()
    {

    }

    void Update()
    {
        
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
       
        yield return null;
        isAttack = false;
    }
}

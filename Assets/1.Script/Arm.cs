using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] string handName;
    [SerializeField] float range;
    [SerializeField] int damage;
    [SerializeField]float attackDelay;
    [SerializeField] float attackDelayA;//공격 활성화
    [SerializeField] float attackDelayB;//공격 비활성화

    [SerializeField]Animator anim;
  
}

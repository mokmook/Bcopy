using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] string handName;
    [SerializeField] float range;
    [SerializeField] int damage;
    [SerializeField]float attackDelay;
    [SerializeField] float attackDelayA;//���� Ȱ��ȭ
    [SerializeField] float attackDelayB;//���� ��Ȱ��ȭ

    [SerializeField]Animator anim;
  
}

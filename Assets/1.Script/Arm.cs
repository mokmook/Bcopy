using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField]ArmData armData;    
}
[System.Serializable]
public struct ArmData
{
    [SerializeField] string handName;
    [SerializeField] float range;
    [SerializeField] int damage;
    [SerializeField] float attackDelay;
    [SerializeField] float attackDelayA;//���� Ȱ��ȭ
    [SerializeField] float attackDelayB;//���� ��Ȱ��ȭ

    [SerializeField] Animator anim;


    public string _handName=>handName;
    public float _range=>range;
    public int _damage=>damage;
    public float _attackDelay=>attackDelay;
    public float _attackDelayA=>attackDelayA;
    public float _attackDelayB=>attackDelayB;
    public Animator _anime => anim;
}


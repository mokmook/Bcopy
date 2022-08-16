using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int sum;
    int[] score = new int[5] { 10, 20, 30, 40, 50 };
    int average;
    void Start()
    {
        /*for (int i = 0; i <= 10; i++)
        {
            if (i%3>0)
            {
                print(i);
                sum += i;
            }
        }
        print(sum);
        */
        foreach (var item in score)
        {
            sum += item;
            average=sum/score.Length;            
        }
        print(average);
        
        for (int i = 0; i < score.Length; i++)
        {
            sum += score[i];
        }
        average=sum/ score.Length;
        print(average);
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Setting : MonoBehaviour
{
    [SerializeField] Transform SettingPanel;
    [SerializeField] Button SettingBtn;
    Button[] SettingButtons;
    [SerializeField] int BtnCount;
    private void Awake()
    {
        SettingButtons = new Button[BtnCount];
    }
    void Start()
    {
        for (int i = 0; i < BtnCount; i++)
        {
            SettingButtons[i]= Instantiate(SettingBtn, transform);
            
        }
    }

    void Update()
    {
        
    }
}

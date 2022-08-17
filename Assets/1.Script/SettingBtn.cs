using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : MonoBehaviour
{
    [SerializeField] GameObject SettingPanel;
    
    void Start()
    {
        SettingPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingActive();
        }
    }
        public void SettingActive()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Setting : MonoBehaviour
{
    [SerializeField] Transform SettingPanel;
    [SerializeField] Button SettingBtn;
    [SerializeField] string[] settingBtnName;
    Button[] SettingButtons;
    private void Awake()
    {
        SettingButtons = new Button[settingBtnName.Length];
        
    }
    void Start()
    {
        for (int i = 0; i < settingBtnName.Length; i++)
        {
            SettingButtons[i]= Instantiate(SettingBtn, SettingPanel);
            SettingButtons[i].GetComponent<RectTransform>().anchoredPosition +=new Vector2(0, i * -200);
            int idx = i;
            SettingButtons[i].onClick.AddListener(() => SettingOnclik(idx));
            SettingButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = settingBtnName[i];

        }
    }
    
  void SettingOnclik(int index)
    {
        switch (index)
        {
            case 0: 
                gameObject.SetActive(false);
                break;
        }
    }
}

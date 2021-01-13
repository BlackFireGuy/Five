using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInHome : BasePanel
{
    Button openInfo;
    Button openSet;

    InfoPanel Info;
    SettingsPanel Set;
    // Start is called before the first frame update
    void Start()
    {
        openInfo = this.GetControl<Button>("OpenInfo");
        openSet = this.GetControl<Button>("OpenSet");

        openInfo.onClick.AddListener(OpenInfoPanel);
        openSet.onClick.AddListener(OpenSetPanel);

        Info = FindObjectOfType<InfoPanel>();
        Set = FindObjectOfType<SettingsPanel>();
    }

    private void OpenInfoPanel()
    {
        Info.mainInfo.SetActive(true);
        //GameSaveManager.instance.LoadGame();
    }

    private void OpenSetPanel()
    {
        Set.menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Info == null||Set== null)
        {
            Info = FindObjectOfType<InfoPanel>();
            Set = FindObjectOfType<SettingsPanel>();
            openInfo.onClick.AddListener(OpenInfoPanel);
            openSet.onClick.AddListener(OpenSetPanel);
        }
    }
}

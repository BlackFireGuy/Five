using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : BasePanel
{
    Button mapButton;
    public  GameObject map;

    bool isOpen;
    private void Start()
    {
        mapButton = this.GetControl<Button>("MapButton");
        //map = this.GetControl<Image>("Map");
        mapButton.onClick.AddListener(OpenMap);
    }

    private void OpenMap()
    {
        if(map != null)
        {
            isOpen = !isOpen;
            map.gameObject.SetActive(isOpen);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenInfo : BasePanel
{
    Button open;
    void Start()
    {
        open = this.GetControl<Button>("OpenInfo");
        open.onClick.AddListener(OpenMainInfo);
    }

    // Update is called once per frame
    public void OpenMainInfo()
    {
        UIManager.GetInstance().ShowPanel<InfoPanel>("InfoPanel", E_UI_Layer.Mid, null);
    }
}

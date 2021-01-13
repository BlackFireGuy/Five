using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : BasePanel
{
    Button pause;
    bool isPause = false;
    private void Start()
    {
        pause = this.GetControl<Button>("Pause Button");
        
        pause.onClick.AddListener(PauseGame);
    }

    public void PauseGame()
    {
        isPause = !isPause;
        if(isPause == true)
        {
            Time.timeScale = 0;
            UIManager.GetInstance().ShowPanel<PauseMenu>("Pause Menu", E_UI_Layer.Null, null);
        }
        else
        {
            Time.timeScale = 1;
            UIManager.GetInstance().HidePanel("Pause Menu");
        }
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNpc1 : DialogButton
{
    public override void Show()
    {
        SkillTreePanel panel = FindObjectOfType<SkillTreePanel>();
        if(panel == null)
        {
            /*UIManager.GetInstance().HidePanel("Skill Tree");*/
            UIManager.GetInstance().ShowPanel<SkillTreePanel>("Skill Tree", E_UI_Layer.Mid, null);
        }
        else
        {
            panel.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }
}

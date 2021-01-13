using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreePanel : BasePanel
{
    [Header("UI")]
    public Image skillImage;
    public Text skillNameTex, skillLvTex, SkillDesTex;
    [Header("Skill Point")]

    public Text pointsText;
    public Button Upgrade;

    public SkillButton[] skillButtons;

    public Button exitButton;
    private void Start()
    {
        SkillManager.instance.skillImage = skillImage;
        SkillManager.instance.skillNameTex = skillNameTex;
        SkillManager.instance.skillLvTex = skillLvTex;
        SkillManager.instance.SkillDesTex = SkillDesTex;
        SkillManager.instance.pointsText = pointsText;
        SkillManager.instance.Upgrade = Upgrade;

        SkillManager.instance.skillButtons = skillButtons;

        
        exitButton.onClick.AddListener(ExistButton);
    }

    private void ExistButton()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }


    private void Update()
    {
        foreach (SkillButton item in skillButtons)
        {
            if (item.skillData.isUnlocked)
            {
                item.transform.GetChild(0).gameObject.SetActive(true);
                item.GetComponentInChildren <Text>().text = item.skillData.skillLevel.ToString();
                item.GetComponent<Image>().color = Color.white;
                
            }
            SkillManager.instance.UpdateUIAndButton();
        }

        
    }
}

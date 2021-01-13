using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    [Header("运行时分配")]
    public SkillData activeSkill;//Click Information
    [Header("UI")]
    public Image skillImage;
    public Text skillNameTex, skillLvTex, SkillDesTex;
    [Header("Skill Point")]
    [SerializeField] public int skillPoint;
    public Text pointsText;
    public Button Upgrade;

    public SkillButton[] skillButtons;
    bool isButtonNotNull = false;
    public void DisPlaySkillInfo()
    {
        skillImage.sprite = activeSkill.skillSprite;
        skillNameTex.text = activeSkill.skillName;
        skillLvTex.text = "天赋等级 ：等级" + activeSkill.skillLevel;
        SkillDesTex.text = "描述：\n" + activeSkill.skillDes;
    }

    public void UpdateUIAndButton()
    {
        UpdatePointUI();
        Upgrade.onClick.RemoveAllListeners();
        Upgrade.onClick.AddListener(UpgradeButton);
    }

    private void Update()
    {
        
        if(Upgrade == null)
        {
            return;
        }
        else
        {
            if (!isButtonNotNull)//没监听过
            {
                UpdatePointUI();
                Upgrade.onClick.AddListener(UpgradeButton);
                isButtonNotNull = true;//建立了监听
            }
        }
        
    }
    private void UpdatePointUI()
    {
        pointsText.text ="天赋 ：" + skillPoint.ToString()+"/20";
    }

    public void UpgradeButton()
    {
        if (activeSkill == null) return;
        //01 1/2
        if(skillPoint > 0&& activeSkill.preSkills.Length == 0)
        {
            UpdateSkill();
        }
        //3-11
        if (skillPoint > 0)
        {
            for (int i = 0; i < activeSkill.preSkills.Length; i++)
            {
                if (activeSkill.preSkills[i].isUnlocked == true)
                {
                    UpdateSkill();
                    break;
                }
            }
        }

        GameSaveManager.instance.SaveGame();
    }

    private void UpdateSkill()
    {
        //1 按钮变亮 技能等级累加
        skillButtons[activeSkill.skillID].GetComponent<Image>().color = Color.white;
        skillButtons[activeSkill.skillID].transform.GetChild(0).gameObject.SetActive(true);
        activeSkill.skillLevel++;
        skillButtons[activeSkill.skillID].transform.GetComponentInChildren<Text>().text = activeSkill.skillLevel.ToString();
        //2显示等级
        DisPlaySkillInfo();
        //3更新

        //4点数减少
        skillPoint--;
        PlayerInfoManager.instance.info.points--;

        UpdatePointUI();
        //5unlocked
        activeSkill.isUnlocked = true;
    }
}

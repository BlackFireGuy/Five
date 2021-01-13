using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Tree/New Skill", fileName ="New Skill")]
[Serializable]
public class SkillData : ScriptableObject
{
    public int skillID;
    public Sprite skillSprite;
    public string skillName;
    public int skillLevel;
    [TextArea(1, 8)]
    public string skillDes;

    public bool isUnlocked;//ifisunlockeed == true;我们可以升级下一个技能 
    public SkillData[] preSkills;//前置技能

}



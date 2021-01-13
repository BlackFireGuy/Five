using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill Tree/New Skill Tree", fileName = "New Skill Tree")]
public class SkillTree : ScriptableObject
{
    public List<SkillData> skillList = new List<SkillData>();
}

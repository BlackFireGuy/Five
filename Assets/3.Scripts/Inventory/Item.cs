using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{ equip,medicine,drug,forever,skill,others}

[Serializable]
public class ItemData
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea]
    public string itemInfo;

    public ItemType types;//类型1：装备；2：回复类药物；3：毒药；4：永久性提高物品；5：技能；6：其他


    [Header("属性加成")]
    public float health;
    public float poisonResistance;
    public float hitResistance;
    public float magicResistance;
    public float attack;
    public float critRate;
    public float critPower;
    public float power;
    public float magic;
    public float speed;


    public bool isEquip;//是否装备装备
    
    [Header("技能编号")]
    public int skillId;//技能编号
    [Header("技能")]
    public float castRange;//施法范围
    public float cooldown;//攻击间隙
    public float damageRange;//伤害范围
    [Header("飞行物")]
    
    public float flySpeed;//飞行速度
    public float radius;//半径
    public float duringTime;//持续时间
    public bool trace;//是否追踪
}

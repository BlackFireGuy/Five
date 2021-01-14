using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InfoPanel : BasePanel
{
    //Button openInfo;
    Button openMain;
    Button openBag;
    Button back;
    //-----------------第一层菜单
    public GameObject main;
    public GameObject bag;
    public GameObject mainInfo;
    //------------------第二层菜单----------------
    public GameObject medicinesPanel;
    public GameObject equipsPanel;
    public GameObject skillsPanel;
    public GameObject booksPanel;
    public GameObject othersPanel;

    //--------------------------个人信息-------------------------
    Text playerName;
    Text level;
    Text health;
    Text poisonResistance;
    Text hitResistance;
    Text magicResistance;
    Text attack;
    Text critRate;
    Text critPower;
    Text power;
    Text magic;
    Text exp;
    Text playerCurrentInfo;
    //-------------------------------
    Text time;
    Text playTime;
    Text pastSuccess;
    Text forentSucces;
    //--------------------------------
    //--------------------------------
    Button skillEquip;
    Text skillInformation;
    //--------------小仓库-------------------
    Button medicines;
    Button equips;
    Button skills;
    Button books;
    Button others;

    //-----------------------------------------
    int hour;
    int minute;
    int second;
    int year;
    int month;
    int day;
    // Start is called before the first frame update
    void Start()
    {
        //-------------------个人信息-----------------
        /*openInfo = this.GetControl<Button>("OpenInfo");
        openInfo.onClick.AddListener(OpenInfo);*/
        //------------------------查看、物品、返回-----------------------
        back = this.GetControl<Button>("back");
        back.onClick.AddListener(OpenInfo);
        openMain = this.GetControl<Button>("openMain");
        openMain.onClick.AddListener(ClickMain);
        openBag = this.GetControl<Button>("openBag");
        openBag.onClick.AddListener(ClickBag);

        //----------------------个人信息------------------------
        playerName = this.GetControl<Text>("name");
        level = this.GetControl<Text>("level");
        playerCurrentInfo = this.GetControl<Text>("playerCurrentInfo");
        health = this.GetControl<Text>("health");

        poisonResistance = this.GetControl<Text>("poisonResistance");
        hitResistance = this.GetControl<Text>("hitResistance");
        magicResistance = this.GetControl<Text>("magicResistance");

        attack = this.GetControl<Text>("attack");
        critRate = this.GetControl<Text>("critRate");
        critPower = this.GetControl<Text>("critPower");

        //power = this.GetControl<Text>("power");
        magic = this.GetControl<Text>("magic");
        exp = this.GetControl<Text>("exp");

        time = this.GetControl<Text>("time");
        playTime = this.GetControl<Text>("playTime");
        pastSuccess = this.GetControl<Text>("pastSuccess");
        forentSucces = this.GetControl<Text>("forentSucces");

        //-------------------------------

        skillEquip = this.GetControl<Button>("skillEquip");
        skillEquip.onClick.AddListener(UnEquipSkill);
        skillInformation = this.GetControl<Text>("skillInformation");
        //-------------------------------

        medicines = this.GetControl<Button>("medicines");
        medicines.onClick.AddListener(OpenMedicines);
        equips = this.GetControl<Button>("equips");
        equips.onClick.AddListener(OpenEquips);
        skills = this.GetControl<Button>("skills");
        skills.onClick.AddListener(OpenSkills);
        books = this.GetControl<Button>("books");
        books.onClick.AddListener(OpenBooks);
        others = this.GetControl<Button>("others");
        others.onClick.AddListener(OpenOthers);
        //---------------------------------
        bag.SetActive(false);
        mainInfo.SetActive(false);
        equipsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        booksPanel.SetActive(false);
        othersPanel.SetActive(false);

        //
    }

    //当打开背包的时候刷新信息 
    private void OnEnable()
    {
        RefreshInfo();
    }

    private void OpenOthers()
    {
        medicinesPanel.SetActive(false);
        equipsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        booksPanel.SetActive(false);
        othersPanel.SetActive(true);
        InventoryManager.RefreshItem();
    }

    private void OpenBooks()
    {
        medicinesPanel.SetActive(false);
        equipsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        booksPanel.SetActive(true);
        othersPanel.SetActive(false);
        InventoryManager.RefreshItem();
    }

    private void OpenSkills()
    {
        medicinesPanel.SetActive(false);
        equipsPanel.SetActive(false);
        skillsPanel.SetActive(true);
        booksPanel.SetActive(false);
        othersPanel.SetActive(false);
        InventoryManager.RefreshItem();
    }

    private void OpenEquips()
    {
        medicinesPanel.SetActive(false);
        equipsPanel.SetActive(true);
        skillsPanel.SetActive(false);
        booksPanel.SetActive(false);
        othersPanel.SetActive(false);
        InventoryManager.RefreshItem();
    }

    private void OpenMedicines()
    {
        
        medicinesPanel.SetActive(true);
        equipsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        booksPanel.SetActive(false);
        othersPanel.SetActive(false);
        InventoryManager.RefreshItem();

    }

    private void ClickBag()
    {
        main.SetActive(false);
        bag.SetActive(true);
        OpenMedicines();
        
    }

    private void ClickMain()
    {
        main.SetActive(true);
        bag.SetActive(false);
        RefreshInfo();
    }

    private void UnEquipSkill()
    {
        if (GameManager.instance.isSkillShoot)//
        {
            //卸掉装备
            //返回背包
            GameManager.instance.isSkillShoot = false;
        }
    }

    void Update()
    {
        //刷新信息页 一会修改到调用再刷新，避免浪费资源
        RefreshInfo();
    }


    private void OpenInfo()
    {
        mainInfo.SetActive(false);

        //打开角色信息时载入角色信息 不是很合理啊，目前只在使用物品的时候才保存
        GameSaveManager.instance.SaveGame();
        //Debug.Log(PlayerInfoManager.instance.info.ToString());
    }

    public void RefreshInfo()
    {
        //判断是否获取到label或者button
        if (playTime == null) return;

        //获取当前时间
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;
        day = DateTime.Now.Day;

        //格式化显示当前时间
        //time.text = string.Format("{0:D2}:{1:D2}:{2:D2} " + "{3:D4}/{4:D2}/{5:D2}", hour, minute, second, year, month, day);
        if(time != null)
            time.text = string.Format("{0:D2}:{1:D2} ", hour, minute);
#if UNITY_EDITOR
        //Debug.Log("W now " + System.DateTime.Now);     //当前时间（年月日时分秒）
        //Debug.Log("W utc " + System.DateTime.UtcNow);  //当前时间（年月日时分秒）
#endif


        //我踏马就把它注掉就可以了！，你妹的为啥啊，草，这个函数会new一个新的，我踏马浪废了这么多时间
        //time.text = (PlayerInfoManager.instance.LoadPlayerInfo().playTime/3600).ToString();

        //---------------
        playerName.text = Playerstate.instance.playerName;
        playTime.text = PlayerInfoManager.instance.info.playTime.ToString();

        
        level.text = Playerstate.instance.playerLevel.ToString();
        exp.text = Playerstate.instance.currentExp.ToString() + "/" + Playerstate.instance.nextlevelExp[Playerstate.instance.playerLevel].ToString();
        //升级加成+装备加成+天赋加成

        //health.text = Playerstate.instance.currentHp + "/"+Playerstate.instance.maxHp+PlayerInfoManager.instance.info.health.ToString();
        health.text = Playerstate.instance.currentHp + "/" + Playerstate.instance.maxHp;
        poisonResistance.text = PlayerInfoManager.instance.info.poisonResistance.ToString();
        hitResistance.text = (Playerstate.instance.defense + PlayerInfoManager.instance.info.hitResistance).ToString();
        magicResistance.text = PlayerInfoManager.instance.info.magicResistance.ToString();

        attack.text = (Playerstate.instance.attack+PlayerInfoManager.instance.info.attack).ToString();
        critRate.text = PlayerInfoManager.instance.info.critRate.ToString();
        critPower.text = PlayerInfoManager.instance.info.critPower.ToString();

        //power.text = PlayerInfoManager.instance.info.power.ToString();
        //magic.text = Playerstate.instance.currentMp + "/" +Playerstate.instance.currentMp + PlayerInfoManager.instance.info.magic.ToString();
        magic.text = Playerstate.instance.currentMp + "/" + Playerstate.instance.maxMp;
        playerCurrentInfo.text = Playerstate.instance.description;

        //刷新技能页面
        //if (GameManager.instance.isSkillShoot)
        //{
        skillEquip.image.sprite = PlayerInfoManager.instance.infoItemData.itemImage;
        if (PlayerInfoManager.instance.infoItemData.itemName == null || PlayerInfoManager.instance.infoItemData.itemName == "")
        {
            skillInformation.text = "";
            skillEquip.image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            skillEquip.image.color = new Color(255, 255, 255, 255);
        }
        if (PlayerInfoManager.instance.infoItemData.castRange > 0)//法术类
        {
            skillInformation.text = "<color=red>" + PlayerInfoManager.instance.infoItemData.itemName + "</color>" +
           "\n攻击加成：+" + PlayerInfoManager.instance.infoItemData.attack +
           "\n攻击间隙： " + PlayerInfoManager.instance.infoItemData.cooldown +
           "\n施法范围： " + PlayerInfoManager.instance.infoItemData.castRange +
           "\n持续时间： " + PlayerInfoManager.instance.infoItemData.duringTime +
           "\n伤害范围： " + PlayerInfoManager.instance.infoItemData.damageRange
           ;
        }
        else//射击类
        {
            skillInformation.text = "<color=red>" + PlayerInfoManager.instance.infoItemData.itemName + "</color>" +
            "\n攻击加成：+" + PlayerInfoManager.instance.infoItemData.attack +
            "\n攻击间隙： " + PlayerInfoManager.instance.infoItemData.cooldown +
            "\n飞行速度： " + PlayerInfoManager.instance.infoItemData.flySpeed +
            "\n持续时间： " + PlayerInfoManager.instance.infoItemData.duringTime +
            "\n  " + PlayerInfoManager.instance.infoItemData.itemInfo
            ;
        }
        //}

    }

}

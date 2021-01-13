using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    Button openBag;
    public GameObject bag;
    public GameObject slotGrid;
    public Text information;
    public Button use;

    public Inventory playerInventory;

    bool isBagOpen = false;
    //------------------------
    HealthBar healthBar;

    public List<GameObject> slots = new List<GameObject>();
    private void Start()
    {
        openBag = this.GetControl<Button>("Open Bag");
        if (openBag != null)
        {

            openBag.onClick.AddListener(OpenBag);
        }

        SetGridItemInformation();

        use.onClick.AddListener(UseItem);
    }
    private void OnEnable()
    {
        SetGridItemInformation();
    }
    public void SetGridItemInformation()
    {
        InventoryManager.instance.slotGrid = slotGrid;
        InventoryManager.instance.itemInformation = information;
        InventoryManager.instance.myBag = playerInventory;
        InventoryManager.instance.slots = slots;
        InventoryManager.instance.use = use;
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }
    }
    //使用物体
    private void UseItem()
    {
        int id = InventoryManager.instance.itemID;
        if (playerInventory.itemList[id] == null) return;

        if(healthBar != null)
        {
            healthBar.UpdateHealth(FindObjectOfType<PlayerController>().health);
        }
        ItemData itemData = playerInventory.itemList[id];
        //去掉对应ID的物品或者物品数量-1//装备或者技能显示装备卸下
        DeleteItem(id);
        //实现对应效果
        FuckingYou(itemData, id);
        InventoryManager.RefreshItem();
        //使用物品时保存玩家信息和仓库信息
        GameSaveManager.instance.SaveGame();
    }

    private void FuckingYou(ItemData itemData,int id)
    {
        
        PlayerInfoManager.instance.UseSomthing(itemData, id);
    }

    private void DeleteItem(int id)
    {
        
        if (playerInventory.itemList[id].types == ItemType.equip || playerInventory.itemList[id].types == ItemType.skill)
        {
            if (playerInventory.itemList[id].isEquip == false)//点击装备
            {
                //use.GetComponentInChildren<Text>().text = "卸下";
                string info = "<color=yellow>" + playerInventory.itemList[id].itemName + "(已装备)" + "</color>" + "\n" + playerInventory.itemList[id].itemInfo;
                InventoryManager.UpdateItemInfo(info, id ,"卸下");
                playerInventory.itemList[id].isEquip = true;
            }
            else//点击卸下
            {
                string info = "<color=yellow>" + playerInventory.itemList[id].itemName+ "</color>" + "\n"+ playerInventory.itemList[id].itemInfo;
                InventoryManager.UpdateItemInfo(info, id, "装备");
                playerInventory.itemList[id].isEquip = false;

            }
            //调整位置
            
            return;
        }
        
        if (playerInventory.itemList[id].itemHeld > 1)
        {
            playerInventory.itemList[id].itemHeld -= 1;
            
        }
        else
        {
            if (playerInventory.itemList[id].itemHeld == 1)
            {

                playerInventory.itemList[id] = new ItemData();
                information.text = "";
            }
        }
        InventoryManager.RefreshItem();
    }
    public void OpenBag()
    {
        isBagOpen = !isBagOpen;
        if (isBagOpen) Time.timeScale = 0;
        else Time.timeScale = 1;
        bag.SetActive(isBagOpen);

        InventoryManager.RefreshItem();
    }

}
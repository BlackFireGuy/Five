using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;//空格ID = 物品ID
    ItemData slotItem;
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;
    public GameObject itemSlot;

    /// <summary>
    /// 点击slot 调用InventoryManager的更新函数，更新物体信息和物体ID
    /// </summary>
    public void ItemOnClick()
    {
        //如果是装备或者技能，在信息栏和使用栏要修改信息
        string info = slotInfo;
        if(InventoryManager.instance.myBag.itemList[slotID].types == ItemType.skill|| InventoryManager.instance.myBag.itemList[slotID].types == ItemType.equip)
        {
            if (InventoryManager.instance.myBag.itemList[slotID].isEquip)
            {
                info = "<color=yellow>"+slotItem.itemName + "(已装备)" + "</color>" + "\n" + slotInfo;
                InventoryManager.UpdateItemInfo(info, slotID, "卸下");
            }
            else
            {
                info = "<color=yellow>" + slotItem.itemName + "</color>" + "\n" + slotInfo;
                InventoryManager.UpdateItemInfo(info, slotID, "装备");
            }
        }
        else
        {
            info = "<color=yellow>" + slotItem.itemName + "</color>" + "\n" + slotInfo;
            InventoryManager.UpdateItemInfo(info, slotID, "使用");
        }

    }

    public void SetupSlot(ItemData item)
    {
        if (item == null)
        {
            itemSlot.SetActive(false);
            return;
        }
        slotItem = item;
        slotImage.sprite = item.itemImage;
        slotInfo = item.itemInfo;
        if (item.itemName == ""|| item.itemName == null)
        {
            slotNum.text = "";
            slotImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            slotNum.text = item.itemHeld.ToString();
        }
    }
}

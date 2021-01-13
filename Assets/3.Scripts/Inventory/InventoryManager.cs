using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Inventory myBag;
    public GameObject emptyslot;
    [Header("运行时分配")]
    public GameObject slotGrid;
    //当前物体的信息和ID
    public Text itemInformation;
    public int itemID;

    public List<GameObject> slots = new List<GameObject>();

    public Button use;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    //当InventoryManager启动时 刷新当前仓库
    private void OnEnable()
    {
        if (instance.slotGrid == null) return;
        RefreshItem();
        instance.itemInformation.text = "";
    }
    //更新商品信息
    public static void UpdateItemInfo(string itemDesc,int Id,string isUsed = null)
    {
        instance.itemInformation.text = itemDesc;

        instance.use.GetComponentInChildren<Text>().text = isUsed;
        instance.itemID = Id;
    }
    // 刷新当前仓库
    public static void RefreshItem()
    {
        if (instance.slotGrid == null) return;
        //循环删除slotgrid下的子集物体
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        //重新生成对应mybag里面的物体的slot
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptyslot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);

        }
    }
}

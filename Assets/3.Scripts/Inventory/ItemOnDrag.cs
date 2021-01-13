using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    //拖拽
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;//当前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        InventoryManager.instance.itemID = currentItemID;
        myBag = InventoryManager.instance.myBag;
        //originalParent.GetComponent<Slot>().ItemOnClick();
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")//判断下面物体名字是：Item Image 那么互换位置 
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //itemlist的物体存储位置改变
                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;
                // 通知InventoryManager当前slotID已经换了
                InventoryManager.instance.itemID = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;//射线阻挡开启，不然无法再次选中物体
                InventoryManager.RefreshItem();
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                //否则直接挂在检测的Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                //itemList的物体存储位置改变
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
                // 通知InventoryManager当前slotID已经换了
                InventoryManager.instance.itemID = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID;
                
                //解决自己放在自己位置的问题
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                    myBag.itemList[currentItemID] = new ItemData();
                
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.RefreshItem();
                return;
            }
        }
        //可以添加移除的逻辑
        //其他任何位置都归还原位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //草，有return ，我说怎么一直不刷新
        InventoryManager.RefreshItem();
    }

    //-------------------------长按------------------
    public float Ping;
    public bool IsStart = false;
    public double LastTime = 0;
    public GameObject infomation;
    void Update()
    {
        //if (IsStart && Ping > 0 && LastTime > 0 && (Time.time - LastTime > Ping))
        if (IsStart && Ping > 0 && LastTime > 0 && ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 - LastTime > Ping))
        {
            //Debug.Log("长按触发");
            infomation.SetActive(true);
            // 显示物品加成
            ShowAdd();
            IsStart = false;
            LastTime = 0;
        }
    }

    private void ShowAdd()
    {
        string info = "<color=yellow>" + "不知道有什么效果" + "</color>" ;

        switch (InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].types)
        {
            case ItemType.equip://装备
                info = "<color=yellow>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].itemName + "</color>" +
                    "<color=red>" + "  (装备)" + "</color>";
                break;
            case ItemType.medicine://回复类药物
                info = "<color=yellow>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].itemName + "</color>" +
                   "<color=red>" + "  (药物)" + "</color>";
                break;
            case ItemType.drug://毒药
                info = "<color=yellow>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].itemName + "</color>" +
                   "<color=red>" + "  (毒)" + "</color>";
                break;
            case ItemType.forever://永久性提高物品
                info = "<color=yellow>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].itemName + "</color>" +
                    "<color=red>" + "  (永久性加成)" + "</color>"+
            "\n攻击力+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].attack + "</size>" +
            "    生命+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].health + "</size>" +
            "\n暴击率+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].critRate + "</size>" +
            "    毒抗+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].poisonResistance + "</size>" +
            "\n暴幅度+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].critPower + "</size>" +
            "    物抗+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].hitResistance + "</size>" +
            "\n力量+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].power + "</size>" +
            "        魔抗+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].magicResistance + "</size>" +
            "\n魔力+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].magic + "</size>";
                break;
            case ItemType.skill://技能
                info = "<color=yellow>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].itemName + "</color>" +
                   "<color=red>" + "  (技能)" + "</color>"+
                 "     攻击+" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].attack + "</size>" +
                 "     攻击间隙：" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].cooldown + "</size>" +
                 "     飞行速度：" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].flySpeed + "</size>" +
                 "     持续时间：" + "<size=120>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].duringTime + "</size>"
                   ;
                break;
            case ItemType.others://其他
                info = "<color=yellow>" + InventoryManager.instance.myBag.itemList[InventoryManager.instance.itemID].itemName + "</color>" +
                   "<color=red>" + "  (其他)" + "</color>";
                break;
        }
        infomation.GetComponent<Text>().text = info;
    }

    public void LongPress(bool bStart)
    {
        IsStart = bStart;
        if (IsStart)
        {
            InventoryManager.instance.itemID = transform.parent.GetComponent<Slot>().slotID;
            transform.parent.GetComponent<Slot>().ItemOnClick();
            LastTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            //Debug.Log("长按开始");
        }
        else if (LastTime != 0)
        {
            LastTime = 0;
            //Debug.Log("长按取消");
        }
        else
        {
            infomation.SetActive(false);
            //Debug.Log("长按抬起");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public ItemData thisItem;

    public Inventory playerInventory;

    bool added;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    private void AddNewItem()
    {
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            //因为我使用掉物品后在原草为生成的item为nul所以需要这一步
            if (playerInventory.itemList[i].itemName == null) { continue; }

            if (playerInventory.itemList[i].itemName.Contains(thisItem.itemName))
            {
                playerInventory.itemList[i].itemHeld += 1;
                added = true;
                break;

            }
        }
        if (!added)
        {
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null || playerInventory.itemList[i].itemName == ""|| playerInventory.itemList[i].itemName == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    //Debug.Log("新捡起物体" + playerInventory.itemList[i].itemName);
                    added = false;
                    break;
                }
            }
        }
        
        InventoryManager.RefreshItem();
    }
}

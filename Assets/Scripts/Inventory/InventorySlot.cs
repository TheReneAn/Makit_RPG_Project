using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    public Image itemIcon;
    public Text itemCount;

    [Header("Variables from the item")]
    public Item thisItem;
    public Inventory thisInventory;

    private bool Useable = false;

    public void Setup (Item newItem, Inventory newInventory)
    {
        thisItem = newItem;
        thisInventory = newInventory;

        if (thisItem)
        {
            // If a player can equip the new item, there is no count number
            if(thisItem.itemType == Item.ItemType.Equip)
            {
                itemIcon.sprite = thisItem.itemIcon;
                itemCount.text = "";
            }
            else
            {
                itemIcon.sprite = thisItem.itemIcon;
                itemCount.text = "" + thisItem.itemCount;
            }
        }
    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            Useable = true;
            thisInventory.ClickItemSlot(Useable, thisItem);
        }
    }
}

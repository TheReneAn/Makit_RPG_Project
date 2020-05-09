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
            itemIcon.sprite = thisItem.itemIcon;
            itemCount.text = "" + thisItem.itemCount;
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

    //public void Additem(Item _item)
    //{
    //    thisItem = _item;

    //    icon.sprite = _item.itemIcon;
    //    if (Item.ItemType.Use == _item.itemType)
    //    {
    //        if (_item.itemCount > 0)
    //        {
    //            itemCount_Text.text = _item.itemCount.ToString();
    //        }
    //        else
    //        {
    //            itemCount_Text.text = "";
    //        }
    //    }
    //}

    //public void RemoveItem()
    //{
    //    icon.sprite = null;
    //    itemCount_Text.text = "";
    //}

    //public string GetitemType()
    //{
    //    return thisItemType = thisItem.itemType.ToString();
    //}

    //public void ClickedOn()
    //{
    //    Inventory.instance.SelectItem(thisItem);
    //}
}

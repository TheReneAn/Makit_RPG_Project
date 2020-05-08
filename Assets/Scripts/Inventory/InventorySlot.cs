using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item thisItem;
    public string thisItemType;

    // Display
    public Image icon;
    public Text itemCount_Text;

    public string itemType;

    public void Additem(Item _item)
    {
        thisItem = _item;

        icon.sprite = _item.itemIcon;
        if (Item.ItemType.Use == _item.itemType)
        {
            if (_item.itemCount > 0)
            {
                itemCount_Text.text = _item.itemCount.ToString();
            }
            else
            {
                itemCount_Text.text = "";
            }
        }
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        itemCount_Text.text = "";
    }

    public string GetitemType()
    {
        return thisItemType = thisItem.itemType.ToString();
    }

    public void ClickedOn()
    {
        Inventory.instance.SelectItem(thisItem);
    }
}

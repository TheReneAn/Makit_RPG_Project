using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
[System.Serializable]
public class Item 
{
    public int itemID;      // The unique ID value of the item.
    public string itemName; // The name of the item.
    public string itemDescription;
    public int itemCount;   // Number of items in possession
    public Sprite itemIcon;

    public ItemType itemType;
    public string _itemType;

    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        Coin
    }

    public Item(int _itemId, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemId;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;
        itemIcon = Resources.Load("ItemIcon/" + _itemName, typeof(Sprite)) as Sprite;
    }
}

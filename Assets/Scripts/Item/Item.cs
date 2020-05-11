using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class Item : ScriptableObject
{
    public int itemID;              // The unique ID value of the item.
    public string itemName;         // The name of the item.
    public string itemDescription;
    public int itemCount = 1;       // Number of items in possession
    public Sprite itemIcon;

    public ItemType itemType;
    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        Coin
    }

    public UnityEvent thisEvent;
    public void Use()
    {
        thisEvent.Invoke();
    }

    public void DecreseAmount(int amountToDecrease)
    {
        itemCount -= amountToDecrease;
        if (itemCount < 0)
        {
            itemCount = 0;
        }
    }
}

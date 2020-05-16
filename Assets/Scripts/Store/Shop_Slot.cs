using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Slot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    public Image itemIcon;
    public Text itemName;
    public Text itemPrice;
    public Text itemOwnCount;
    public Text show_itemqty;
    public int int_itemqty;

    [Header("Variables from the others")]
    public Item thisItem;
    public UI_Store_Sell thisStoreSell;

    public void SellSlot_Setup(Item newItem, UI_Store_Sell newStoreSell)
    {
        int_itemqty = 0;
        thisItem = newItem;
        thisStoreSell = newStoreSell;

        if (thisItem)
        {
            if (thisItem.itemType == Item.ItemType.Use)
            {
                itemIcon.sprite = thisItem.itemIcon;
                itemName.text = thisItem.itemName;
                itemPrice.text = thisItem.itemPrice.ToString();
                itemOwnCount.text = "" + thisItem.itemCount;
            }

            if (thisItem.itemType == Item.ItemType.Equip)
            {
                itemIcon.sprite = thisItem.itemIcon;
                itemName.text = thisItem.itemName;
                itemPrice.text = thisItem.itemPrice.ToString();
                itemOwnCount.text = "" + 1;
            }
        }
    }

    public void Btn_Qty_up()
    {
        int_itemqty += 1;

        if (int_itemqty >= 999)
        {
            int_itemqty = 999;
        }
        else if (int_itemqty > thisItem.itemCount)
        {
            int_itemqty = thisItem.itemCount;
        }
        else
        {
            thisStoreSell.int_TotalMoney_Sell += thisItem.itemPrice;
        }
    }

    public void Btn_Qty_down()
    {
        int_itemqty -= 1;

        if (int_itemqty < 0)
        {
            int_itemqty = 0;
        }
        else
        {
            thisStoreSell.int_TotalMoney_Sell -= thisItem.itemPrice;
        }
    }

    public int Get_item_itemqty()
    {
        return thisItem.itemCount -= int_itemqty;
    }

    // Update is called once per frame
    void Update()
    {
        show_itemqty.text = int_itemqty.ToString();
        itemOwnCount.text = "" + thisItem.itemCount;
    }
}

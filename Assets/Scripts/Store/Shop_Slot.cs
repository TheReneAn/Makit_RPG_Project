using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Slot : MonoBehaviour
{
    bool StoreBuyAction = false;

    [Header("UI Stuff to change")]
    public Image itemIcon;
    public Text itemName;
    // Price
    public Text itemPrice;
    public int temp_itemPrice;
    // QTY
    public Text itemOwnCount;
    public Text show_itemqty;
    public int int_itemqty;

    [Header("Variables from the others")]
    public Item thisItem;
    private UI_Store_Sell thisStoreSell;
    private UI_Store_Buy thisStoreBuy;

    [Header("Audio")]
    public string errorSound;

    public void SellSlot_Setup(Item newItem, UI_Store_Sell newStoreSell)
    {
        StoreBuyAction = false;

        int_itemqty = 0;
        thisItem = newItem;
        thisStoreSell = newStoreSell;

        if (thisItem)
        {
            // When a user sell a item, can not receive the original price
            temp_itemPrice = (int)(thisItem.itemPrice * 0.8);

            if (thisItem.itemType == Item.ItemType.Use)
            {
                itemIcon.sprite = thisItem.itemIcon;
                itemName.text = thisItem.itemName;
                itemPrice.text = temp_itemPrice.ToString();
                itemOwnCount.text = "" + thisItem.itemCount;
            }

            if (thisItem.itemType == Item.ItemType.Equip)
            {
                itemIcon.sprite = thisItem.itemIcon;
                itemName.text = thisItem.itemName;
                itemPrice.text = temp_itemPrice.ToString();
                itemOwnCount.text = "" + 1;
            }
        }
    }

    public void BuySlot_Setup(Item newItem, UI_Store_Buy newStoreBuy)
    {
        StoreBuyAction = true;

        int_itemqty = 0;
        thisItem = newItem;
        thisStoreBuy = newStoreBuy;

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

        // Item Qty Max
        if (int_itemqty >= 999)
        {
            int_itemqty = 999;
            AudioManager.Instance.Play(errorSound);
        }

        // Sell
        if (StoreBuyAction != true)
        {
            // Users cannot sell more items than they have.
            if (int_itemqty > thisItem.itemCount)
            {
                int_itemqty = thisItem.itemCount;
                AudioManager.Instance.Play(errorSound);
            }
            else
            {
                thisStoreSell.int_TotalMoney_Sell += thisItem.itemPrice;
            }
        }
        
        // Buy
        if (StoreBuyAction == true)
        {
            thisStoreBuy.int_TotalMoney_Buy += thisItem.itemPrice;

            // Users cannot buy more items than they have money.
            if (thisStoreBuy.int_TotalMoney_Buy > GameManager.Instance.g_Money)
            {
                thisStoreBuy.int_TotalMoney_Buy -= thisItem.itemPrice;
                int_itemqty -= 1;
                AudioManager.Instance.Play(errorSound);
            }
        }
    }

    public void Btn_Qty_down()
    {
        int_itemqty -= 1;

        if (int_itemqty < 0)
        {
            int_itemqty = 0;
            AudioManager.Instance.Play(errorSound);
        }
        else
        {
            // Sell
            if (StoreBuyAction != true)
            {
                thisStoreSell.int_TotalMoney_Sell -= thisItem.itemPrice;
            }

            // Buy
            if (StoreBuyAction == true)
            {
                thisStoreBuy.int_TotalMoney_Buy -= thisItem.itemPrice;
            }
        }
    }

    public int Get_current_ownqty()
    {
        return thisItem.itemCount;
    }

    // Update is called once per frame
    void Update()
    {
        show_itemqty.text = int_itemqty.ToString();

        Get_current_ownqty();
        itemOwnCount.text = "" + Get_current_ownqty();
    }
}

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
    private int int_itemqty = 0;

    [Header("Variables from the others")]
    public Item thisItem;

    public void Sell_Setup(Item newItem)
    {
        thisItem = newItem;

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
    }

    public void Btn_Qty_down()
    {
        int_itemqty -= 1;

        if (int_itemqty <= 0)
        {
            int_itemqty = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Setting item qty
        int_itemqty = 0;
    }

    // Update is called once per frame
    void Update()
    {
        show_itemqty.text = int_itemqty.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store_Buy : MonoBehaviour
{
    public int WhichStore;  // 1000: General, 1001: Armor

    [Header("UI Stuff to change")]
    public Image thisStoreNPC;
    public Sprite[] Temp_NPC_sprite;

    [Header("Money")]
    public Text Text_TotalMoney_Buy;
    public int int_TotalMoney_Buy;
    public Text Text_Playesmoney;

    [Header("Variables from the other")]
    public Player player;
    public PlayerInventory PlayerInventory;
    public db_Use_item DB_Use_itemList;
    public db_Use_item DB_Equip_itemList;

    [Header("Store buy Object Information")]
    public GameObject blankStoreBuySlot;
    public GameObject StoreBuyPanel;
    public Transform BuyListParent;

    public List<GameObject> slots = new List<GameObject>();

    public void Setup(int Select_Action_NPC_ID, Image newthisStoreNPC)
    {
        WhichStore = Select_Action_NPC_ID;
        if (WhichStore == 1000)  // 1000: Genaral Store 
        {
            thisStoreNPC.sprite = Temp_NPC_sprite[0];
        }
        else if (WhichStore == 1001)
        {
            thisStoreNPC.sprite = Temp_NPC_sprite[1];
        }
    }

    public void BtnBuyIntheUI()
    {
        GameManager.Instance.g_Money -= int_TotalMoney_Buy;

        for (int i = 0; i < slots.Count; i++)
        {
            Shop_Slot slot = slots[i].GetComponent<Shop_Slot>();
            slot.thisItem.itemCount += slot.int_itemqty;
            slot.int_itemqty = 0;
        }

        // Reset
        int_TotalMoney_Buy = 0;
    }

    public void CloseStoreBuyUI()
    {
        StoreBuyPanel.SetActive(false);
        ClearInventorySlots();
        player.CanMove();

        // Reset
        int_TotalMoney_Buy = 0;
    }

    public void MakeStoreBuySlots()
    {
        // 1000: General
        if (WhichStore == 1000)
        {
            for (int i = 0; i < DB_Use_itemList.db_itemList.Count; i++)
            {
                if (DB_Use_itemList.db_itemList[i].itemType == Item.ItemType.Use)
                {
                    GameObject StoreBuytemp = Instantiate(blankStoreBuySlot, BuyListParent);
                    Shop_Slot newStoreBuySlot = StoreBuytemp.GetComponent<Shop_Slot>();
                    slots.Add(StoreBuytemp);

                    if (newStoreBuySlot)
                    {
                        newStoreBuySlot.BuySlot_Setup(DB_Use_itemList.db_itemList[i], this);
                    }
                }
            }
        }

        // 1001: Armor
        else if (WhichStore == 1001)
        {
            for (int i = 0; i < DB_Equip_itemList.db_itemList.Count; i++)
            {
                if (DB_Equip_itemList.db_itemList[i].itemType == Item.ItemType.Equip)
                {
                    GameObject StoreBuytemp = Instantiate(blankStoreBuySlot, BuyListParent);
                    Shop_Slot newStoreBuySlot = StoreBuytemp.GetComponent<Shop_Slot>();

                    if (newStoreBuySlot)
                    {
                        newStoreBuySlot.BuySlot_Setup(DB_Equip_itemList.db_itemList[i], this);
                    }
                }
            }
        }
    }

    public void ClearInventorySlots()
    {
        for (int i = 0; i < BuyListParent.transform.childCount; i++)
        {
            Destroy(BuyListParent.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text_TotalMoney_Buy.text = int_TotalMoney_Buy.ToString();
        if (int_TotalMoney_Buy < 0)
        {
            int_TotalMoney_Buy = 0;
        }

        Text_Playesmoney.text = "" + GameManager.Instance.g_Money;
        if (GameManager.Instance.g_Money < 0)
        {
            GameManager.Instance.g_Money = 0;
        }
    }
}

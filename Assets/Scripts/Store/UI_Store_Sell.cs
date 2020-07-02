using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store_Sell : MonoBehaviour
{
    public int WhichStore;  // 1000: General, 1001: Armor

    [Header("UI Stuff to change")]
    public Image thisStoreNPC;
    public Sprite[] Temp_NPC_sprite;

    [Header("Money")]
    public Text Text_TotalMoney_Sell;
    public int int_TotalMoney_Sell;
    public Text Text_Playesmoney;

    [Header("Variables from the other")]
    public Player player;
    public PlayerInventory PlayerInventory;

    [Header("Store Sell Object Information")]
    public GameObject blankStoreSellSlot;
    public GameObject StoreSellPanel;
    public Transform SellListParent;

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

    public void BtnSellIntheUI()
    {
        GameManager.Instance.g_Money += int_TotalMoney_Sell;

        // thisitem count - qty
        for (int i = 0; i < slots.Count; i++)
        {
            Shop_Slot slot = slots[i].GetComponent<Shop_Slot>();
            slot.thisItem.itemCount = slot.Get_current_ownqty();
            slot.thisItem.itemCount -= slot.int_itemqty;
            slot.int_itemqty = 0;

            if (slot.thisItem.itemCount <= 0)
            {
                slots.RemoveAt(i);
                ClearInventorySlots();
                MakeStoreSellSlots();
            }
        }

        // Reset
        int_TotalMoney_Sell = 0;
    }

    public void CloseStoreSellUI()
    {
        StoreSellPanel.SetActive(false);
        ClearInventorySlots();
        player.CanMove();

        // Reset
        int_TotalMoney_Sell = 0;
    }

    public void MakeStoreSellSlots()
    {
        if (PlayerInventory)
        {
            for (int i = 0; i < PlayerInventory.myInventory.Count; i++)
            {
                // 1000: General
                if (WhichStore == 1000)
                {
                    if (PlayerInventory.myInventory[i].itemType == Item.ItemType.Use)
                    {
                        GameObject StoreSelltemp = Instantiate(blankStoreSellSlot, SellListParent);
                        Shop_Slot newStoreSellSlot = StoreSelltemp.GetComponent<Shop_Slot>();
                        slots.Add(StoreSelltemp);

                        if (newStoreSellSlot)
                        {
                            newStoreSellSlot.SellSlot_Setup(PlayerInventory.myInventory[i], this);
                        }
                    }

                }
                // 1001: Armor
                else if (WhichStore == 1001)
                {
                    if (PlayerInventory.myInventory[i].itemType == Item.ItemType.Equip)
                    {
                        GameObject StoreSelltemp = Instantiate(blankStoreSellSlot, SellListParent);
                        Shop_Slot newStoreSellSlot = StoreSelltemp.GetComponent<Shop_Slot>();

                        if (newStoreSellSlot)
                        {
                            newStoreSellSlot.SellSlot_Setup(PlayerInventory.myInventory[i], this);
                        }
                    }
                }
            }
        }
    }

    public void ClearInventorySlots()
    {
        for (int i = 0; i < SellListParent.transform.childCount; i++)
        {
            Destroy(SellListParent.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text_TotalMoney_Sell.text = int_TotalMoney_Sell.ToString();
        if (int_TotalMoney_Sell < 0)
        {
            int_TotalMoney_Sell = 0;
        }

        Text_Playesmoney.text = "" + GameManager.Instance.g_Money;
        if (GameManager.Instance.g_Money < 0)
        {
            GameManager.Instance.g_Money = 0;
        }
    }
}

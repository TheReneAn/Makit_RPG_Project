using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store_Sell : MonoBehaviour
{
    public int WhichStore;  // 1000: General, 1001: Armor

    [Header("UI Stuff to change")]
    public Text Text_TotalMoney_Sell;

    [Header("Variables from the other")]
    public Player player;
    public PlayerInventory PlayerInventory;

    [Header("Store Sell Information")]
    public GameObject blankStoreSellSlot;
    public GameObject StoreSellPanel;
    private bool activeStoreSellUI = false;
    public Transform SellListParent;

    public void Setup(int Select_Action_NPC_ID)
    {
        WhichStore = Select_Action_NPC_ID;
    }

    public void CloseStoreSellUI()
    {
        activeStoreSellUI = !activeStoreSellUI;

        if (activeStoreSellUI == true)
        {
            StoreSellPanel.SetActive(false);
            player.CanMove();
        }


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

                        if (newStoreSellSlot)
                        {
                            newStoreSellSlot.Sell_Setup(PlayerInventory.myInventory[i]);
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
                            newStoreSellSlot.Sell_Setup(PlayerInventory.myInventory[i]);
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

    }
}

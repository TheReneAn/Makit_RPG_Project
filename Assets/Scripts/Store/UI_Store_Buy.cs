using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store_Buy : MonoBehaviour
{
    public int WhichStore;  // 1000: General, 1001: Armor

    [Header("UI Stuff to change")]
    public Image thisStoreNPC;
    public Sprite[] Temp_NPC_sprite;

    [Header("Variables from the other")]
    public Player player;
    public PlayerInventory PlayerInventory;

    [Header("Store Sell Information")]
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}

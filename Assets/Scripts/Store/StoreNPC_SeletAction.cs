using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreNPC_SeletAction : MonoBehaviour
{
    [Header("NPC Information")]
    public int NPC_ID;
    public Text NPC_Name;
    public Image ShowNPC;

    public Sprite[] Temp_NPC_sprite;

    public GameObject StoreSellPanel;

    [Header("Variables from the other")]
    private Player player;
    private StoreNPC_Phsical this_StoreNPC_Phsical;
    public UI_Store_Sell ui_store_sell;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.CanNotMove();
    }

    public void Setup(StoreNPC_Phsical newPhsical)
    {
        this_StoreNPC_Phsical = newPhsical;

        NPC_ID = this_StoreNPC_Phsical.NPC_ID;
        NPC_Name.text = this_StoreNPC_Phsical.NPC_Name;

        if (NPC_ID == 1000)  // 1000: Genaral Store 
        {
            ShowNPC.sprite = Temp_NPC_sprite[0];
        }
        else if (NPC_ID == 1001)
        {
            ShowNPC.sprite = Temp_NPC_sprite[1];
        }
    }

    public void Btn_Buy()
    {
        // Close this panel
        this_StoreNPC_Phsical.StoreNPC_SelectAction_Panel.SetActive(false);

        Debug.Log("Buy");
    }

    public void Btn_Sell()
    {
        // Close this panel
        this_StoreNPC_Phsical.StoreNPC_SelectAction_Panel.SetActive(false);

        ui_store_sell.Setup(NPC_ID);
        ui_store_sell.CloseStoreSellUI();
        ui_store_sell.MakeStoreSellSlots();
        StoreSellPanel.SetActive(true);
    }

    public void Btn_Close()
    {
        this_StoreNPC_Phsical.StoreNPC_SelectAction_Panel.SetActive(false);

        player.CanMove();
    }
}

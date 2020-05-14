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

    private Player player;
    private StoreNPC_Phsical this_StoreNPC_Phsical;

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
        else
        {
            ShowNPC.sprite = Temp_NPC_sprite[1];
        }
    }

    public void Btn_Buy()
    {
        Debug.Log("Buy");

        // Close this panel
        Btn_Close();
    }

    public void Btn_Sell()
    {
        Debug.Log("Sell");

        // Close this panel
        Btn_Close();
    }

    public void Btn_Close()
    {
        if (NPC_ID == 1000) // 1000: Genaral Store 
        {
            this_StoreNPC_Phsical.SetActive_Select_Panel();
        }
        else // 1001: Armor Store 
        {
            this_StoreNPC_Phsical.SetActive_Select_Panel();
        }

        player.CanMove();
    }
}

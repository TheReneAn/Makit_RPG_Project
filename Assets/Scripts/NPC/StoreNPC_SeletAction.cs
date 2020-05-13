using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreNPC_SeletAction : MonoBehaviour
{
    [Header("NPC Information")]
    private int NPC_ID;
    public Text NPC_Name;
    public Image ShowNPC;

    public Sprite[] Temp_NPC_sprite;

    private Player player;
    public StoreNPC_Phsical this_StoreNPC_Phsical;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        player.CanNotMove();

        NPC_ID = this_StoreNPC_Phsical.NPC_ID;
        NPC_Name.text = this_StoreNPC_Phsical.NPC_Name;

        if(NPC_ID == 1000)  // 1000: Genaral Store 
        {
            ShowNPC.sprite = Temp_NPC_sprite[0];
        }
        else
        {
            ShowNPC.sprite = Temp_NPC_sprite[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

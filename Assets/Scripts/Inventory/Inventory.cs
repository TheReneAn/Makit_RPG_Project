using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory PlayerInventory;
    private OkorCancel theOOC;

    public GameObject blankInventorySlot;
    public GameObject inventoryPanel;
    public Transform itemParent;
    public Text Text_Coin;
    public GameObject OkorCanclePanel;

    public Item currentItem;

    // Audio
    private AudioManager theAudio;
    public string open_sound;
    public string cancel_sound;
    public string click_sound;
    public string beep_sound;       // error sound

    void MakeInventorySlots()
    {
        if (PlayerInventory)
        {
            for (int i = 0; i < PlayerInventory.myInventory.Count; i++)
            {
                GameObject temp = Instantiate(blankInventorySlot, itemParent);
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                if (newSlot)
                {
                    newSlot.Setup(PlayerInventory.myInventory[i], this);
                }

            }
        }
    }

    public void ClickItemSlot (bool _clickedSlot, Item newItem)
    {
        currentItem = newItem;
        if((currentItem.itemType.ToString() == "Use") || 
            (currentItem.itemType.ToString() == "Equip"))

        {
            OkorCanclePanel.SetActive(_clickedSlot);
        }
        else
        {
            theAudio.Play(beep_sound);
        }
    } 

    // Start is called before the first frame update
    void Start()
    {
        theOOC = FindObjectOfType<OkorCancel>();

        Text_Coin.text = string.Format("{0:n0}", GameManager.Instance.g_Money);

        MakeInventorySlots();

        theAudio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

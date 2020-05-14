using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Information")]
    public Player player;
    public PlayerInventory PlayerInventory;

    public GameObject blankInventorySlot;
    public GameObject inventoryPanel;
    private bool activeInventory = false;
    public Transform itemParent;

    public Text Text_Coin;
    public GameObject OkorCanclePanel;

    public Item currentItem;

    [Header("Inventory Audio")]
    private AudioManager theAudio;
    public string open_sound;
    public string cancel_sound;
    public string click_sound;
    public string beep_sound;       // error sound

    public string enter_sound;

    public void ActiveInventory()
    {
        activeInventory = !activeInventory;

        if (activeInventory)
        {
            theAudio.Play(open_sound);
            player.CanNotMove();
            inventoryPanel.SetActive(true);
        }
        else
        {
            theAudio.Play(cancel_sound);
            inventoryPanel.SetActive(false);
            player.CanMove();
        }
    }

    public void MakeInventorySlots()
    {
        if (PlayerInventory)
        {
            for (int i = 0; i < PlayerInventory.myInventory.Count; i++)
            {
                if (PlayerInventory.myInventory[i].itemCount > 0)
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
    }

    public void ClearInventorySlots()
    {
        for(int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
    }

    public void ClickItemSlot (Item newItem)
    {
        currentItem = newItem;

        if((currentItem.itemType.ToString() == "Use") || 
            (currentItem.itemType.ToString() == "Equip"))

        {
            OkorCanclePanel.SetActive(true);
            theAudio.Play(enter_sound);
        }
        else
        {
            theAudio.Play(beep_sound);
            OkorCanclePanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
    }

    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Text_Coin.text = string.Format("{0:n0}", GameManager.Instance.g_Money);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public Player player;
    private OkorCancel theOOC;

    // Panel
    public GameObject inventoryPanel;   //  Inventory open & close
    private bool activeInventory = false;
    public GameObject OkorcanclePanel;  // Question open & close
    private bool SelectItem_Actived = false;

    // Coin
    public Text coin_text;

    // Audio
    private AudioManager theAudio;
    public string open_sound;
    public string cancel_sound;
    public string click_sound;
    public string beep_sound;       // error sound

    // Slots
    private InventorySlot[] slots;  // Inventory slots
    public Transform itemsParent;    // Slot's Parents object 
    private List<Item> inventoryItemList;   // Player's item list

    public string thisItemType;
    public int thisItemID;

    private bool stopKeyInput = false;  // Key input limit 
                                        // in the ask question window when a player use a item

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        instance = this;

        player.GetComponent<Player>();
        theOOC = FindObjectOfType<OkorCancel>();

        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        coin_text.text = string.Format("{0:n0}", GameManager.Instance.g_Money);

        if (!stopKeyInput)
        {
            if (activeInventory)
            {
                // Display item lists
                ShowItem();

                if (SelectItem_Actived) // Select slot and Show Okay or Cancel
                {
                    StartCoroutine(OOCCoroution());
                }   
            }
        }
    }

    public void ActiveInventory()
    {
        if (!stopKeyInput)
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
    }

    public void GetAnItem(int _itemID, string itemType, int _count = 1)
    {
        // Search item database
        for (int i = 0; i < ItemManager.Instance.itemList.Count; i++)
        {
            // Check if there is a matching item 
            if (_itemID == ItemManager.Instance.itemList[i].itemID)
            {
                // Item Type is Coin
                if (itemType == Item.ItemType.Coin.ToString())
                {
                    GameManager.Instance.g_Money += _count;
                    return;
                }

                // Am I already have the item?
                for (int j = 0; j < inventoryItemList.Count; j++)
                {
                    if (inventoryItemList[j].itemID == _itemID)
                    {
                        // Item Type is Use, Quest
                        if ((inventoryItemList[j].itemType == Item.ItemType.Use) ||
                           (inventoryItemList[j].itemType == Item.ItemType.Quest))
                        {
                            inventoryItemList[j].itemCount += _count;
                        }
                        // Item Type is Equip
                        if (inventoryItemList[j].itemType == Item.ItemType.Equip)
                        {
                            inventoryItemList.Add(ItemManager.Instance.itemList[i]);
                        }
                        return;
                    }
                }

                // I don't have the item
                inventoryItemList.Add(ItemManager.Instance.itemList[i]);
                inventoryItemList[inventoryItemList.Count - 1].itemCount = _count;
                return;
            }
        }

        Debug.LogError("No item with that ID.");
    }

    public void ShowItem()
    {
        RemoveSlot();

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryItemList[i]);
        }
    }

    public void RemoveSlot()    // Inventory slot list reset
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }

    public void SelectItem(Item _thisItem)  // From InventorySlot 
    {
        theAudio.Play(click_sound);
        stopKeyInput = true;
        
        thisItemType = _thisItem.itemType.ToString();
        thisItemID = _thisItem.itemID;

        SelectItem_Actived = true;
    }

    IEnumerator OOCCoroution()
    {
        OkorcanclePanel.SetActive(true);
        theOOC.activated = true;
        
        yield return new WaitUntil(() => !theOOC.activated);

        if (theOOC.GetResult()) // Okay btn (Want to use)
        {
            // Search Item list
            for (int i = 0; i < inventoryItemList.Count; i++)
            {
                // Check itemID between the slot's item id and  the inventory item id.
                if (inventoryItemList[i].itemID == thisItemID)
                {
                    // If the item has counts
                    if (inventoryItemList[i].itemCount > 1)
                    {
                        inventoryItemList[i].itemCount--;
                    }
                    // If the item hasn't counts
                    else
                    {
                        inventoryItemList.RemoveAt(i);
                    }

                    ShowItem();
                    break;
                }
            }
        }

        stopKeyInput = false;
        OkorcanclePanel.SetActive(false);
    }
}

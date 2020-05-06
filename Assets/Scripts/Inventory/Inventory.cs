using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;

    public GameObject inventoryPanel;   //  Inventory open & close
    private bool activeInventory = false;

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

    private bool stopKeyInput = false;  // Key input limit 
                                        // in the ask question window when a player use a item

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start()
    {
        player.GetComponent<Player>();

        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
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

    //public void SelectedItem()
    //{
    //    Color color = slots[0].selected_Item.GetComponent<Image>().color;
    //    color.a = 0f;
    //    for (int i = 0; i < inventoryItemList.Count; i++)
    //    {
    //        slots[i].selected_Item.GetComponent<Image>().color = color;
    //        StartCoroutine(SelectedItemEffectCoroutine());
    //    }
    //}

    //IEnumerator SelectedItemEffectCoroutine()
    //{
    //    while (itemActivated)
    //    {
    //        Color color = slots[0].GetComponent<Image>().color;
    //        while (color.a < 0.5f)
    //        {
    //            color.a += 0.03f;
    //            slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
    //            yield return waitTime;
    //        }
    //        while (color.a > 0f)
    //        {
    //            color.a -= 0.03f;
    //            slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
    //            yield return waitTime;
    //        }
    //    }

    //    yield return new WaitForSeconds(0.03f);
    //}

    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                activeInventory = !activeInventory;

                if (activeInventory)
                {
                    theAudio.Play(open_sound);
                    player.NotMove();
                    inventoryPanel.SetActive(true);
                }
                else
                {
                    theAudio.Play(cancel_sound);
                    inventoryPanel.SetActive(false);
                    player.Move();
                }
            }

            if (activeInventory)
            {
                ShowItem();

                if (Input.GetMouseButtonDown(0)) // Select a item
                {
                    theAudio.Play(click_sound);
                    stopKeyInput = true;
                    // Use Question (Ex. really want to drink the potion?)

                    // Equip Question (Ex. really want to drink the potion?)

                }
            }
        }
    }
}

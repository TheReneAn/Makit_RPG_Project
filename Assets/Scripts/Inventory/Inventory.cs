using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private AudioManager theAudio;
    public string open_sound;
    public string click_sound;
    public string beep_sound;       // error sound

    private InventorySlot[] slots;  // Inventory slots
    private List<Item> inventoryItemList;   // Player's item list

    public Transform itemsParent;    // Slot's Parents object 
    public GameObject go;   //  Inventory open & close

    // select
    private int selectedItem;
    private bool activated;     // 



}

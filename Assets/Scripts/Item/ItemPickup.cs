using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;
    public string itemType;
    public int _count;
    public string pickUpSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        AudioManager.Instance.Play(pickUpSound);
        // Add the item to inventory
        Inventory.instance.GetAnItem(itemID, itemType, _count);

        Destroy(this.gameObject);
    }
}

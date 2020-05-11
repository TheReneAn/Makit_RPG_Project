using UnityEngine;

public class Physical_Item : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Item thisItem;

    [Header("Audio")]
    public string pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.Play(pickUpSound);
            AddItemToInventory();
            Destroy(this.gameObject);
        }
    }

    void AddItemToInventory()
    {
        if(playerInventory && thisItem)
        {
            if (playerInventory.myInventory.Contains(thisItem))
            {
                // Equip items
                if(thisItem.itemType == Item.ItemType.Equip)
                {
                    playerInventory.myInventory.Add(thisItem);
                }
                else
                {
                    thisItem.itemCount += 1;
                }
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.itemCount += 1;
            }
        }
    }
}

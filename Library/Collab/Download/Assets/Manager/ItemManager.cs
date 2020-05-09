using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region Singleton
    private static ItemManager instance = null;

    // Don't destroy
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // singleton
    public static ItemManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    public List<Item> itemList = new List<Item>();

    void Start()
    {
        // Coin
        itemList.Add(new Item(999999, "Golden Coin", "A Coin made of gold", Item.ItemType.Coin));

        // Use (Potion)
        itemList.Add(new Item(100001, "Small Red Potion", "A miracle potion that restores 50 HP", Item.ItemType.Use));
        itemList.Add(new Item(100002, "Medium Red Potion", "A miracle potion that restores 150 HP", Item.ItemType.Use));
        itemList.Add(new Item(100003, "Big Red Potion", "A miracle potion that restores 350 HP", Item.ItemType.Use));
        itemList.Add(new Item(100004, "Small Blue Potion", "A miracle potion that restores 50 MP", Item.ItemType.Use));
        itemList.Add(new Item(100005, "Medium Blue Potion", "A miracle potion that restores 150 MP", Item.ItemType.Use));
        itemList.Add(new Item(100006, "Big Blue Potion", "A miracle potion that restores 350 MP", Item.ItemType.Use));

        // Equip (Weapon & Tool)
        itemList.Add(new Item(200001, "Wooden Sword", "A beginner's sword", Item.ItemType.Equip));
        itemList.Add(new Item(200002, "Silver Sword", "It looks a little strong", Item.ItemType.Equip));
        itemList.Add(new Item(200003, "Iron Sword", "It can cut anything", Item.ItemType.Equip));
        itemList.Add(new Item(200004, "Golden Sword", "The strongest sword in existence even feels confident", Item.ItemType.Equip));
        itemList.Add(new Item(200005, "Knife", "It's stronger than you thought", Item.ItemType.Equip));

        itemList.Add(new Item(200006, "Wooden Armor", "It breaks as easily as it is easy to get", Item.ItemType.Equip));
        itemList.Add(new Item(200007, "Leather Armor", "It is comfortable and light", Item.ItemType.Equip));
        itemList.Add(new Item(200008, "Iron Armor", "Heavy but strong armor", Item.ItemType.Equip));

        // Quest
        itemList.Add(new Item(300001, "Paper", "paper that is the material of a book.", Item.ItemType.Quest));
        itemList.Add(new Item(300002, "Wooden Plank", "Piece of a Treem living near the village", Item.ItemType.Quest));
        itemList.Add(new Item(300003, "Leather", "leather that can be used as a cover for a book", Item.ItemType.Quest));
        itemList.Add(new Item(300004, "String", "A special string made by May's mother", Item.ItemType.Quest));
        itemList.Add(new Item(300005, "Book", "A mysterious book that can change fate", Item.ItemType.Quest));
        itemList.Add(new Item(300006, "Golden Key", "A key made of gold", Item.ItemType.Quest));
    }
}
  
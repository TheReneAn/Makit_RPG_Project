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

    private void Start()
    {
        // Potion
        itemList.Add(new Item(100001, "Small Red Potion", "A miracle potion that restores 50 HP", Item.ItemType.Use));
        itemList.Add(new Item(100002, "Medium Red Potion", "A miracle potion that restores 150 HP", Item.ItemType.Use));
        itemList.Add(new Item(100003, "Big Red Potion", "A miracle potion that restores 350 HP", Item.ItemType.Use));
        itemList.Add(new Item(100004, "Small Blue Potion", "A miracle potion that restores 50 MP", Item.ItemType.Use));
        itemList.Add(new Item(100005, "Medium Blue Potion", "A miracle potion that restores 150 MP", Item.ItemType.Use));
        itemList.Add(new Item(100006, "Big Blue Potion", "A miracle potion that restores 350 MP", Item.ItemType.Use));

        // Weapon & Tool
        itemList.Add(new Item(200001, "Wooden Sword", "A beginner's sword. Str +5", Item.ItemType.Equip));

        // Quest
        itemList.Add(new Item(300001, "Wool", "Sheep fur", Item.ItemType.Quest));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DB_item", menuName = "Inventory/DB_itemList")]
public class db_Use_item : ScriptableObject
{
    public List<Item> db_itemList = new List<Item>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> InventoryItems = new Dictionary<string, int>();
    
    public void AddItem(string itemName, int amount)
    {
        InventoryItems.Add(itemName, amount);
    }

    public void RemoveItem(string itemName)
    {
        InventoryItems.Remove(itemName);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void AddItem(string itemName)
    {
        if (!HasItem(itemName))
            items.Add(itemName);
    }
}

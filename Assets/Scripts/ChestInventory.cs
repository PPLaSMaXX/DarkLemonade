using System.Collections.Generic;
using UnityEngine;

public  class ChestInventory
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"> Додано в інвентар: {item.GetFullName()}");
    }

    public void PrintInventory()
    {
        Debug.Log("=== Вміст інвентаря ===");
        foreach (Item item in items)
        {
            Debug.Log(item.GetFullName());
            Debug.Log(item.GetDescription());
        }
    }
}

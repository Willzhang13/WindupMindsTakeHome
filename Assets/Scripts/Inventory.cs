using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<IPickable> items;
    public event Action<List<IPickable>> OnInventoryChanged;

    void Start()
    {
        items = new List<IPickable>();
    }

    public void AddItem(IPickable item)
    {
        items.Add(item);
        item.OnPickUp();
        OnInventoryChanged?.Invoke(items);
        Debug.Log("Item added to inventory: " + item);
    }

    public IPickable RemoveItem()
    {
        if (items.Count == 0) return null;
        IPickable item = items[0];
        items.RemoveAt(0);
        OnInventoryChanged?.Invoke(items);
        return item;
    }

    public bool HasItem()
    {
        return items.Count > 0;
    }
}

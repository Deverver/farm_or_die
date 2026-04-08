using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Test – startfrø")]
    [SerializeField] private ItemData[] startingItems;
    [SerializeField] private int startingAmount = 5;

    private Dictionary<ItemData, int> items = new();
    public static event System.Action OnInventoryChanged;

    void Start()
    {
        foreach (var item in startingItems)
            AddItem(item, startingAmount);
    }

    public void AddItem(ItemData item, int amount = 1)
    {
        items.TryGetValue(item, out int current);
        items[item] = current + amount;
        OnInventoryChanged?.Invoke();
    }

    public bool RemoveItem(ItemData item, int amount = 1)
    {
        if (!HasItem(item, amount)) return false;
        items[item] -= amount;
        if (items[item] <= 0) items.Remove(item);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool HasItem(ItemData item, int amount = 1)
        => items.TryGetValue(item, out int c) && c >= amount;

    public int GetCount(ItemData item)
        => items.TryGetValue(item, out int c) ? c : 0;

    public Dictionary<ItemData, int> GetAll() => new(items); // kopi
}

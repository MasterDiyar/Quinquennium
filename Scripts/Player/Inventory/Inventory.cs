using System;
using System.Collections.Generic;
using Godot;
using Quinquennium.Scripts.Items;
using Quinquennium.Scripts.Resources;

namespace Quinquennium.Scripts.Player.Inventory;

public partial class Inventory : Node
{
    private List<ItemInInventory> _items = [];
    public List<ItemInInventory> GetItems() => _items;

    public void AddItem(ItemResource itemR, int count)
    {
        if (count <= 0) return;

        foreach (var i in _items) {
            if (i.ItemResource.Id != itemR.Id || i.Amount >= i.ItemResource.MaxCount) continue;
            int remainingSpace = i.ItemResource.MaxCount - i.Amount;

            if (remainingSpace >= count) {
                i.Amount += count;
                count = 0; 
                break;
            }
            i.Amount = i.ItemResource.MaxCount;
            count -= remainingSpace;
        }

        while (count > 0) {
            int amountForNewStack = Math.Min(count, itemR.MaxCount);
            _items.Add(CreateNewItem(itemR, amountForNewStack));
            count -= amountForNewStack;
        }
    }

    public bool RemoveItem(ItemResource itemR, int count)
    {
        int totalAmount = 0;
        foreach (var i in _items) {
            if (i.ItemResource.Id == itemR.Id) 
                totalAmount += i.Amount;
        }

        if (totalAmount < count) return false;

        for (int i = _items.Count - 1; i >= 0; i--) {
            if (_items[i].ItemResource.Id == itemR.Id) {
                if (_items[i].Amount > count) {
                    _items[i].Amount -= count;
                    break; 
                }
                count -= _items[i].Amount;
                _items.RemoveAt(i);
            }
            if (count <= 0) break;
        }

        return true; 
    }

    public void SplitStackHalf(int index)
    {
        if (index < 0 || index >= _items.Count) return;
        ItemInInventory originalItem = _items[index];
        if (originalItem.Amount <= 1) return;

        int half = originalItem.Amount / 2; 
        originalItem.Amount -= half;        

        _items.Add(CreateNewItem(originalItem.ItemResource, half));
    }

    private ItemInInventory CreateNewItem(ItemResource itemR, int count)
    {
        ItemInInventory item = new ItemInInventory();
        item.ItemResource = itemR;
        item.Amount = count;
        return item;
    }
}
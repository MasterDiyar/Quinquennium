<H1>Player Inventory System</H1>

[Back](../../../README.md)

    Core system responsible for managing player items, stacking logic, splitting, and resource removal.


```C#
private List<ItemInInventory> _items = [];
```
The internal storage holding all items currently carried by the player.
Functions
```C#
void AddItem(ItemResource itemR, int count);
```
Converts ItemResource to ItemInInventory. 
First, it loops through existing items to fill up incomplete stacks up to their MaxCount.
If there is still a remaining count, 
creates new item stacks using CreateNewItem and appends them to the list.
```C#
bool RemoveItem(ItemResource itemR, int count);
```

Removes a specified amount of items from the inventory (useful for crafting or dropping).
It first checks if the total count across all stacks is sufficient.
If true, it removes items starting from the end of the list, completely deleting empty stacks.
Returns false if there aren't enough items.
```C#
void SplitStackHalf(int index);
```
Splits a stack at the given list index in half (typically for UI right-click mechanics). 
Calculates half the amount (rounding down for odd numbers), 
subtracts it from the original stack, 
and creates a new stack with the split amount at the end of the inventory.
```C#
ItemInInventory CreateNewItem(ItemResource itemR, int count);
```
A private helper method that instantiates a new ItemInInventory container, 
assigns its resource type, and sets the initial item amount.
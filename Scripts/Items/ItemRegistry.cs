using Godot;
using System.Collections.Generic;

public partial class ItemRegistry : Node
{
    private readonly Dictionary<string, ItemResource> _items = new();

    public override void _Ready()
    {
        LoadItemsFromFolder("res://Items/");
    }

    private void LoadItemsFromFolder(string path)
    {
        using var dir = DirAccess.Open(path);
        if (dir == null) return;

        dir.ListDirBegin();
        string fileName = dir.GetNext();

        while (fileName != "") {
            if (!dir.CurrentIsDir() && fileName.EndsWith(".tres")) {
                var item = ResourceLoader.Load<ItemResource>($"{path}/{fileName}");
                if (item != null && !string.IsNullOrEmpty(item.Id)) {
                    if (!_items.TryAdd(item.Id, item))
                        GD.PrintErr($"Конфликт ID! Дубликат идентификатора: {item.Id}");
                    
                }
            }
            fileName = dir.GetNext();
        }
    }

    public ItemResource GetItem(string id)
    {
        if (_items.TryGetValue(id, out var item))
            return item;
        
        GD.PrintErr($"Предмет с ID '{id}' не найден в реестре!");
        return null;
    }
}
using Godot;
using Quinquennium.Scripts.Items;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public string Id {get; set;}
    [Export] public string DisplayName { get;  set; }
    [Export] public Texture2D Icon { get;  set; }
    [Export] public string Description { get;  set; }
    [Export] public int MaxCount { get;  set; }

    public void SetItem(Item item)
    {
        item.Name = DisplayName;
        
    }
}
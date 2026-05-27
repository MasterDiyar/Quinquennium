using Godot;

namespace Quinquennium.Scripts.Items;

public partial class Item : Sprite2D
{
    [Export] public string DisplayName;
    [Export] public string Description;
    [Export] public int Count = 1;
    [Export] public int MaxCount;
    [Export] public ItemResource ItemResource;
    public bool isSetted = false;
    public override void _Ready()
    {
        if (!isSetted && ItemResource != null)
        {
            var clone = (ItemResource)ItemResource.Duplicate();
            clone.SetItem(this);
        } 
    }
}
using Godot;

namespace Quinquennium.Scripts.Resources;

[GlobalClass]
public partial class ItemInInventory : Resource
{
    [Export] public ItemResource ItemResource;
    [Export] public int Amount = 0;
}
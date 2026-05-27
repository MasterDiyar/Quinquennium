using Godot;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export] public string Id {get; set;}
    [Export] public string DisplayName { get; private set; }
    [Export] public Texture2D Icon { get; private set; }
    [Export] public string Description { get; private set; }
    [Export] public int MaxCount { get; private set; }
}
using Godot;

namespace Quinquennium.Scripts.Items;

public partial class Item : Node2D
{
    private PackedScene Texture;

    public override void _Ready()
    {
        AddChild(Texture.Instantiate());
    }
}
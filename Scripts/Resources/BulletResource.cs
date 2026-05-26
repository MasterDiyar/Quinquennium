
using Godot;

[GlobalClass]
public partial class BulletResource : Resource
{
    [Export] public PackedScene TextureScene { get; set; }
    [Export] public PackedScene BulletScene  { get; set; }
    [Export] public float Speed { get; set; }
    [Export] public float LifeTime { get; set; }
    [Export] public PackedScene OnDie { get; set; }
    [Export] public PackedScene OnFly { get; set; }
}
using Godot;

namespace Quinquennium.Scripts.Weapon;

public partial class Weapon : Node2D
{
    [Export] public WeaponResource WeaponRes;
    
    public PackedScene BulletScene,
                       ParticleScene;
    public       float AttackSpeed, 
                       ExecuteSpeed,
                       Damage;
    public         int BulletCount;

    public override void _Ready()
    {
        
        if (WeaponRes == null) { GD.PrintErr($"No weapon resource on: {Name}"); return; }
        WeaponRes = (WeaponResource)WeaponRes.Duplicate();
        WeaponRes.SetWeapon(this);
    }


    public virtual void Execute()
    {
        
    }
}
using Godot;
using Quinquennium.Scripts.Interfaces;
using Quinquennium.Scripts.Items;

namespace Quinquennium.Scripts.Weapon;

public partial class Weapon : Node2D, IInteractable
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
        if (GetParent() is Item item) item.InteractableItem = this;
    }


    public virtual void Execute(BulletResource bulletRes, Vector2 spawnPosition, float rotationAngle)
    {
        for(int i = 0; i < BulletCount; i++){
            BulletBase bullet = bulletRes.BulletScene.Instantiate<BulletBase>();

            bullet.GlobalPosition = spawnPosition;
            bullet.Rotation = rotationAngle;

            GameManager.Instance.Pausable.AddChild(bullet);

            bullet.Init(bulletRes);
        }
    }

    public void OnLeftClick(Vector2 mousePosition, Vector2 interactPosition)
    {
        var angle = interactPosition - mousePosition;
        Execute(WeaponRes.bullet, mousePosition, angle.Angle());
    }

    public void OnRightClick(Vector2 mousePosition, Vector2 interactPosition)
    {
        
    }
}
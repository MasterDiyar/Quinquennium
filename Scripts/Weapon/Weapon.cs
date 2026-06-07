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

    private bool mayShoot = true;

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

    public async void OnLeftClick(Vector2 mousePosition, Vector2 interactPosition)
    {
        if (!mayShoot) return;
        mayShoot = false;
        aftershoot = WeaponRes.TimePerAttack;
        if (WeaponRes.ExecuteTime > 0) await ToSignal(GetTree().CreateTimer(WeaponRes.ExecuteTime), "timeout");
        if (!IsInstanceValid(this)) return;
        var angle = - interactPosition + mousePosition;
        Execute(WeaponRes.bullet, interactPosition, angle.Angle());
    }

    public void OnRightClick(Vector2 mousePosition, Vector2 interactPosition)
    {
        
    }

    private float aftershoot = 0;
    public override void _Process(double delta)
    {
        if (aftershoot < 0) return;
        aftershoot -= (float)delta;
    }
}
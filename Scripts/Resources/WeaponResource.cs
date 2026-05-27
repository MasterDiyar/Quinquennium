using Godot;
using Quinquennium.Scripts.Weapon;

[GlobalClass]
public partial class WeaponResource : Resource
{
    [Export] public BulletResource bullet;
    [Export] public PackedScene OnFire;
    [Export] public float Damage, TimePerAttack, ExecuteTime;
    [Export] public int ExecuteCount;
    [Export] public PackedScene WeaponInstanceScene;
    public void SetWeapon(Weapon weapon)
    {
        weapon.ExecuteSpeed = ExecuteTime;
        weapon.AttackSpeed = TimePerAttack;
        weapon.Damage = Damage;
        weapon.BulletScene = bullet.BulletScene;
        weapon.ParticleScene = OnFire;
        weapon.BulletCount = ExecuteCount;
    }
}
using Godot;
using Quinquennium.Scripts.Enums;
using Quinquennium.Scripts.Interfaces;

namespace Quinquennium.Scripts.Unit;

public partial class UnitBase : CharacterBody2D, IDamagable, IUnitChangable
{
    [Export]public UnitResource BaseResource { get; set; }
     [Export]public UnitResource UpgradeResource { get; set; }
    public float Hp { get; set; }
    public void TakeDamage(float damage, Node Unit, DamageType damageType)
    {
    }

    public void OnDie()
    {
    }

    
}
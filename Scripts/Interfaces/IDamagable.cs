using Godot;
using Quinquennium.Scripts.Enums;

namespace Quinquennium.Scripts.Interfaces;

public interface IDamagable
{
    float Hp { get; set; }
    void TakeDamage(float damage, Node Unit, DamageType damageType);
    void OnDie();
}
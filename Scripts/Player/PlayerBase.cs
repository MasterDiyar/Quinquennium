using Godot;
using System;
using Quinquennium.Scripts.Enums;
using Quinquennium.Scripts.Interfaces;

public partial class PlayerBase : CharacterBody2D, IDamagable, IUnitChangable
{
	[Export] public UnitResource BaseResource { get; set; }
	[Export] public UnitResource UpgradeResource { get; set; }
	public float Hp { get; set; }
	[Export] private float Speed = 0;
	[Export] public float Acceleration = 10f; 
	[Export] public float Friction = 15f;     

	public override void _PhysicsProcess(double delta)
	{
		var dt = (float)delta;

		var direction = Input.GetVector("a", "d", "w", "s");
		Vector2 targetVelocity = direction * Speed;
		float weight = direction.Length() > 0 ? Acceleration : Friction;

		Velocity = Velocity.Lerp(targetVelocity, weight * dt);

		MoveAndSlide();
	}

	public void TakeDamage(float damage, Node Unit, DamageType damageType)
	{
		throw new NotImplementedException();
		if (Hp <= 0) OnDie();
	}

	public void OnDie()
	{
		QueueFree();
	}
}

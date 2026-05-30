using Godot;
using System;
using Quinquennium.Scripts.Enums;
using Quinquennium.Scripts.Interfaces;
using Quinquennium.Scripts.Player.Inventory;

public partial class PlayerBase : CharacterBody2D, IDamagable, IUnitChangable
{
	[Export] public UnitResource BaseResource { get; set; }
	[Export] public UnitResource UpgradeResource { get; set; }
	[Export] public Sprite2D Texture;
	public float Hp { get; set; }
	[Export] private float Speed = 0;
	[Export] public float Acceleration = 10f; 
	[Export] public float Friction = 15f;    
	
	[Export] public Inventory inventory;

	public override void _PhysicsProcess(double delta)
	{
		var dt = (float)delta;

		MoveScript(dt);
		RotateTovard();
	}

	protected virtual void MoveScript(float dt)
	{
		var direction = Input.GetVector("a", "d", "w", "s");
        		Vector2 targetVelocity = direction * Speed;
        		float weight = direction.Length() > 0 ? Acceleration : Friction;
        
        		Velocity = Velocity.Lerp(targetVelocity, weight * dt);
        
        		MoveAndSlide();
	}

	protected virtual void RotateTovard()
	{
		var mp = GetGlobalMousePosition();
		Texture.FlipH = (mp.X < GlobalPosition.X);
	}

	public void TakeDamage(float damage, Node Unit, DamageType damageType)
	{
		Hp -= damage;
		if (Hp <= 0) OnDie();
	}

	public void OnDie()
	{
		QueueFree();
	}
}

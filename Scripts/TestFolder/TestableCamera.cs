using Godot;
using System;

public partial class TestableCamera : Camera2D
{
	[Export] public bool IsManualMove = true;
	[Export] public float Speed = 100f;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsManualMove) ManualMove((float)delta);
		
	}

	void ManualMove(float delta)
	{
		Vector2 vector = Input.GetVector("a", "d", "w", "s").Normalized();
		Position += vector * delta * Speed;
		Zoom = (Input.IsActionPressed("q")) ? Zoom : Zoom * (1 + delta);
		Zoom = (Input.IsActionPressed("e")) ? Zoom : Zoom * (1 - delta);
	}
}

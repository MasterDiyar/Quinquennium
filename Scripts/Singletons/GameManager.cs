using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance;
	public Node2D Pausable;
	public override void _Ready()
	{
		if (Instance == null)
			Instance = this;
		else QueueFree();
	}
}

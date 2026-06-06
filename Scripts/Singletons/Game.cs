using Godot;
using System;

public partial class Game : Node2D
{
	public override void _Ready()
	{
		GameManager.Instance.Pausable = GetNode<Node2D>("Pausable");
	}

}

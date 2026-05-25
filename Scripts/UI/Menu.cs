using Godot;
using System;

public partial class Menu : Control
{
	[Export] public Button Play;
	public override void _Ready()
	{
		Play.Pressed += PlayOnPressed;
	}

	private void PlayOnPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/TestFolder/test_game.tscn");
	}
}

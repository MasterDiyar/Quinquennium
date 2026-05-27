using Godot;
using System;
using Quinquennium.Scripts.Items;

public partial class ThrowedItem : Area2D
{
	[Export] public Item LayedItem;
	[Export] public ItemResource ItemResource;
	public override void _Ready()
	{
		if (ItemResource == null || LayedItem == null) return;
		var clone = (ItemResource)ItemResource.Duplicate();
		clone.SetItem(LayedItem);

		BodyEntered += BodyEnter;
	}

	public void BodyEnter(Node2D body)
	{
		
	}
}

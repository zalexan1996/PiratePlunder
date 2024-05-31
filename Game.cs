using Godot;
using System;

public partial class Game : Node2D
{
	private BountyService BountyService => GetTree().GetAutoLoad().BountyService;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BountyService.Reset();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

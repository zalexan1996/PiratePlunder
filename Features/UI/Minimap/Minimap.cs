using Godot;
using System;

public partial class Minimap : Control
{
	[Export]
	public Player Player { get; set; }

	[Export]
	public Sprite2D Sprite2D { get; set; }

    public override void _Process(double delta)
    {
		Sprite2D.Position = Player.Position;
    }
}

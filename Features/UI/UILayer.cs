using Godot;
using System;

public partial class UILayer : CanvasLayer
{
	public GameOverScreen GameOverScreen { get; set; }

	[Export]
	public PackedScene GameOverScreenScene { get; set; }

	[Export]
	public Player Player { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player.OnDeath += onPlayerDeath;
	}

	private void onPlayerDeath()
	{
		GameOverScreen = GameOverScreenScene.Instantiate<GameOverScreen>();

		AddChild(GameOverScreen);
	}
}

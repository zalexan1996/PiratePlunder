using Godot;
using System;

public partial class SpawnerService : Node
{
	[Export]
	public PackedScene CannonballScene { get; set; }

	public Cannonball SpawnCannonball(Vector2 position, Action<Cannonball> builder)
	{
		var cannonball = CannonballScene.Instantiate<Cannonball>();

		GetTree().CurrentScene.AddChild(cannonball);
		cannonball.StartingLocation = position;
		builder(cannonball);

		return cannonball;
	}
}

using Godot;
using System;

public partial class SpawnerService : Node
{
	[Export]
	public PackedScene CannonballScene { get; set; }

	[Export]
	public PackedScene WreckageScene { get; set; }

	public Cannonball SpawnCannonball(Vector2 position, Action<Cannonball> builder)
	{
		var cannonball = CannonballScene.Instantiate<Cannonball>();

		GetTree().CurrentScene.AddChild(cannonball);
		cannonball.StartingLocation = position;
		builder(cannonball);

		return cannonball;
	}

	public Wreckage SpawnWreckage(Vector2 position, float rotation, LootRatioResource lootRatios, Action<Wreckage> builder = null)
	{
		var wreckage = WreckageScene.Instantiate<Wreckage>();

		GetTree().CurrentScene.CallDeferred(MethodName.AddChild, wreckage);
		wreckage.GlobalPosition = position;
		wreckage.GlobalRotation = rotation;
		wreckage.LootRatios = lootRatios;

		if (builder is not null)
		{
			builder(wreckage);
		}

		return wreckage;
	}
}

using Godot;
using System;

public partial class SpawnerService : Node
{
	[Export]
	public PackedScene CannonballScene { get; set; }

	[Export]
	public PackedScene WreckageScene { get; set; }

	[Export]
	public PackedScene ShockwaveScene { get; set; }

	protected T Spawn<T>(PackedScene scene, Action<T> builder = null) where T : Node2D
	{
		var instance = scene.Instantiate<T>();
		GetTree().CurrentScene.AddChild(instance);
		
		builder?.Invoke(instance);

		return instance;
	}
	public Cannonball SpawnCannonball(Vector2 position, Action<Cannonball> builder = null)
	{
		return Spawn(CannonballScene, (Cannonball c) => {
			c.StartingLocation = position;
			c.GlobalPosition = position;
			builder?.Invoke(c);
		});
	}

	public Wreckage SpawnWreckage(Vector2 position, float rotation, LootRatioResource lootRatios, Action<Wreckage> builder = null)
	{
		var instance = WreckageScene.Instantiate<Wreckage>();
		GetTree().CurrentScene.CallDeferred(MethodName.AddChild, instance);

		instance.GlobalPosition = position;
		instance.GlobalRotation = rotation;
		instance.LootRatios = lootRatios;

		builder?.Invoke(instance);
		return instance;
	}

	public Shockwave SpawnShockwave(Vector2 position, Action<Shockwave> builder = null)
	{
		return Spawn(ShockwaveScene, (Shockwave s) => {
			s.GlobalPosition = position;

			builder?.Invoke(s);
		});
	}
}

using Godot;

public partial class Cannon : Node2D
{
	[Export]
	public Node2D CannonballSpawn { get; set; }

	public void Fire(Vector2 TargetLocation, Vector2 InitialVelocity, Node2D owner)
	{
        var cannonball = GetTree().GetAutoLoad().SpawnerService.SpawnCannonball(CannonballSpawn.GlobalPosition, c => {
			c.TargetLocation = TargetLocation;
			c.Owner = owner;
			c.VelocityComponent.ApplyVelocity(InitialVelocity);
		});

	}
}

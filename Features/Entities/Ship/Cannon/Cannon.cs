using Godot;

public partial class Cannon : Node2D
{
	[Signal]
	public delegate void FiredEventHandler();

	[Signal]
	public delegate void ReloadedEventHandler();

	[Export]
	public Node2D CannonballSpawn { get; set; }

	[Export]
	public Timer ReloadTimer { get; set; }

	[Export]
	public float ReloadDelay { get; set; } = 1.0f;
	private bool canFire = true;
	public bool CanFire() => canFire;

	public void Fire(Vector2 TargetLocation, Vector2 InitialVelocity, Node2D owner)
	{
		if (!CanFire())
		{
			return;
		}
        var cannonball = GetTree().GetAutoLoad().SpawnerService.SpawnCannonball(CannonballSpawn.GlobalPosition, c => {
			c.TargetLocation = TargetLocation;
			c.Owner = owner;
			c.VelocityComponent.ApplyVelocity(InitialVelocity);
		});

		canFire = false;
		ReloadTimer.Start(ReloadDelay);
	}

	private void onTimerOver()
	{
		canFire = true;
	}
}
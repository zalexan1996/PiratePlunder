using Godot;
using System;
using System.Diagnostics;

[Tool]
public partial class ProximityDetectionComponent : Area2D
{
	[Signal]
	public delegate void PlayerDetectedEventHandler(Player player);
	[Signal]
	public delegate void PlayerLeftEventHandler(Player player);

	[Export]
	public float DetectionRadius { get; set; } = 150f;

	[Export]
	public CollisionShape2D CollisionShape2D { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var circle = CollisionShape2D.Shape as CircleShape2D;

		if (circle is not null)
		{
			circle.Radius = DetectionRadius;
		}
	}

	private void bodyEntered(Node2D body)
	{
		var player = body as Player;

		if (player is null)
		{
			return;
		}

		EmitSignal(SignalName.PlayerDetected, player);
        Debug.WriteLine(body.Name);
	}

	private void bodyExited(Node2D body)
	{
		var player = body as Player;

		if (player is null)
		{
			return;
		}

		EmitSignal(SignalName.PlayerLeft, player);
	}
}

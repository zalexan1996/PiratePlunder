using Godot;
using System;
using System.Diagnostics;

public partial class Cannonball : Area2D
{
	[Export]
	public PackedScene ExplosionScene { get; set; }

	[Export]
	public Node2D Owner { get; set; }

	[Export]
	public Vector2 StartingLocation 
	{
		get
		{
			return _startingLocation;
		}
		set
		{
			_startingLocation = value;
			GlobalPosition = value;
		}
	}
	private Vector2 _startingLocation;

	[Export]
	public Vector2 TargetLocation { get; set; }

	[Export]
	public VelocityComponent VelocityComponent { get; set; }

	protected Vector2 Direction => (TargetLocation - StartingLocation).Normalized();

	protected FactionResource OwnerFaction => ((IInFaction)Owner).GetFaction();

    public override void _PhysicsProcess(double delta)
    {
		VelocityComponent.ApplyInputVector(Direction);
		
		Position += VelocityComponent.CurrentVelocity * (float)delta;

		if (GlobalPosition.DistanceTo(TargetLocation) < 5)
		{
			explode();
		}
    }

	private void explode()
	{
		var explosion = ExplosionScene.Instantiate<Explosion>();
		explosion.GlobalPosition = GlobalPosition;
		GetTree().CurrentScene.CallDeferred(MethodName.AddChild, explosion);
		CallDeferred(MethodName.QueueFree);
	}


	private void onBodyEntered(Node2D body)
	{
		var damageableBody = body as ITakesDamage;
		var inFaction = body as IInFaction;
		if (damageableBody == null || damageableBody == Owner)
		{
			return;
		}

		if (inFaction is not null && inFaction.IsInFaction(OwnerFaction))
		{
			return;
		}

		damageableBody.TakeDamage(1);
		explode();
	}
}

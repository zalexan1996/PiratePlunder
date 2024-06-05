using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

[Tool]
public partial class ProximityDetectionComponent : Area2D
{
	public event EntityDetectedEventHandler EntityDetected;
	public event EntityLeftEventHandler EntityLeft;
	public delegate void EntityDetectedEventHandler(IEntity entity);
	public delegate void EntityLeftEventHandler(IEntity entity);

	[Export]
	public float DetectionRadius { get; set; } = 150f;

	[Export]
	public CollisionShape2D CollisionShape2D { get; set; }

	public IEntity EntityOwner { get; set; }

	private ICollection<IEntity> EntitiesInProximity = new Collection<IEntity>();

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
		var entity = body as IEntity;

		if (entity is null || !EntityOwner.IsHostileWith(entity))
		{
			return;
		}

		EntityDetected?.Invoke(entity);

		if (!EntitiesInProximity.Contains(entity))
		{
			EntitiesInProximity.Add(entity);
		}
	}

	private void bodyExited(Node2D body)
	{
		var entity = body as IEntity;

		if (entity is null || !EntityOwner.IsHostileWith(entity))
		{
			return;
		}

		if (EntitiesInProximity.Contains(entity))
		{
			EntitiesInProximity.Remove(entity);
		}
		EntityLeft?.Invoke(entity);
	}

	public IEntity GetNextEntityInProximity()
	{
		return EntitiesInProximity.FirstOrDefault();
	}

	public IEntity GetNextHostileEntityInProximity()
	{
		return EntitiesInProximity.FirstOrDefault(e => e.IsHostileWith(EntityOwner));
	}
}

using Godot;
using System;

public partial class GroundCannon : CharacterBody2D, IEntity
{
	[Signal]
	public delegate void DestroyedEventHandler(GroundCannon cannon);

	[Export]
	public FactionResource FactionResource { get; set; }

	[Export]
	public Cannon Cannon { get; set; }
    public InventoryComponent InventoryComponent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public FactionResource GetFaction() => FactionResource;

    public bool IsHostileWith(IEntity entity) => !entity.IsInFaction(FactionResource);

    public void TakeDamage(int damage)
    {
		GetTree().GetAutoLoad().SpawnerService.SpawnShockwave(GlobalPosition);
		Visible = false;
		EmitSignal(SignalName.Destroyed, this);
    }

	public void LookAt(IEntity entity)
	{
		Rotation = (entity.GlobalPosition - GlobalPosition).Rotated(-Rotation).Angle();
	}
}

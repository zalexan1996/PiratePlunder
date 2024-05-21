using Godot;
using System;

public partial class Guy : CharacterBody2D, IEntity
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

    [Export]
    public PackedScene BloodExplosionScene { get; set; }

    [Export]
    public FactionComponent FactionComponent { get; set; }

    public FactionResource GetFaction() => FactionComponent.Faction;


    public void TakeDamage(int damage)
    {
		HealthComponent.TakeDamage(1);
        if (HealthComponent.IsDead())
        {
            var explosion = BloodExplosionScene.Instantiate<BloodExplosion>();
            GetTree().CurrentScene.AddChild(explosion);
            explosion.GlobalPosition = GlobalPosition;
            QueueFree();
        }
    }

    public bool IsHostileWith(IEntity entity)
    {
        return entity.IsInFaction(GetFaction());
    }
}

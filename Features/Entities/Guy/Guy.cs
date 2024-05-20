using Godot;
using System;

public partial class Guy : CharacterBody2D, ITakesDamage
{
	[Export]
	public HealthComponent HealthComponent { get; set; }

    [Export]
    public PackedScene BloodExplosionScene { get; set; }
    
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

}

using Godot;
using System;

public partial class Guy : CharacterBody2D, ITakesDamage
{
	[Export]
	public HealthComponent HealthComponent { get; set; }
    
    public void TakeDamage(int damage)
    {
		HealthComponent.TakeDamage(1);
        if (HealthComponent.IsDead())
        {
            QueueFree();
        }
    }

}

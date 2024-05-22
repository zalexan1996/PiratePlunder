using Godot;
using System;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void HealthChangedEventHandler(int newHealth, int maxHealth);

	[Export]
	public int MaxHealth { get; set; } = 3;

	[Export]
	public int CurrentHealth { get; set; } = 3;


	public void ResetHealth()
	{
		CurrentHealth = MaxHealth;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}

	public bool IsDead() => CurrentHealth == 0;

	public void TakeDamage(int damage)
	{
		CurrentHealth = Math.Clamp(CurrentHealth - damage, 0, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}
}

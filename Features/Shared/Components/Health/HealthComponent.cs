using Godot;
using System;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void HealthChangedEventHandler(float newHealth, float maxHealth);

	[Export]
	public float MaxHealth { get; set; } = 3;

	[Export]
	public float CurrentHealth { get; set; } = 3;

	public bool IsMaxHealth() => CurrentHealth == MaxHealth;
	public void Heal(float additional)
	{
		CurrentHealth += additional;
		CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}

	public void ResetHealth()
	{
		CurrentHealth = MaxHealth;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}

	public bool IsDead() => CurrentHealth == 0;

	public void TakeDamage(float damage)
	{
		CurrentHealth = Math.Clamp(CurrentHealth - damage, 0, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}
}

using Godot;
using System;
using System.Diagnostics;

public partial class HealthDisplayComponent : ProgressBar
{
	[Export]
	public Timer DisplayTimer { get; set; }

	[Export]
	public HealthComponent HealthComponent { get; set; }

	StyleBoxFlat styleBox;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		styleBox = new StyleBoxFlat();
		HealthComponent.HealthChanged += onHealthChange;
		Value = HealthComponent.CurrentHealth;
		MinValue = 0;
		MaxValue = HealthComponent.MaxHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateValue(int newValue, bool show = true)
	{
		Value = newValue;
		AddThemeStyleboxOverride("fill", styleBox);
		styleBox.BgColor = new Color(Mathf.Lerp(1f, 0f, (float)Value / (float)MaxValue), Mathf.Lerp(0f, 1f, (float)Value / (float)MaxValue), 0);
		
		if (show)
		{
        	Show();
		}
	}

	private void onHealthChange(int newHealth)
	{
		UpdateValue(newHealth);
	}

	public void Show()
	{
		Visible = true;
		DisplayTimer.Start();
	}

	public void Hide()
	{
		Visible = false;
	}
}

using Godot;
using System;
using System.Diagnostics;

public partial class EnemyInfoWidget : Control
{
	[Export]
	public Enemy Enemy { get; set; }

	[Export]
	public Label FactionLabel { get; set; }

	[Export]
	public Label ShipTypeLabel { get; set; }

	[Export]
	public Timer Timer { get; set; }

    public override void _Ready()
    {
		FactionLabel.Text = Enemy.ShipData.FactionResource.FactionName;
		ShipTypeLabel.Text = Enemy.ShipData.ShipType.ShipTypeName;
		Timer.Timeout += Hide;
    }

	public void Show()
	{
		Timer.Start(5);
		Visible = true;
	}

	public void Hide()
	{
		Visible = false;
	}
}

using Godot;
using System;
using System.Diagnostics;

public partial class Wreckage : StaticBody2D, IInteractable
{
	[Export]
	public StopwatchComponent StopwatchComponent { get; set; }

	[Export]
	public Node2D LabelPivot { get; set; }

	[Export]
	public int ExistDuration { get; set; } = 30;

	[Export]
	public LootRatioResource LootRatios { get; set; }

	[Export]
	public InventoryComponent InventoryComponent { get; set; }

    public override void _Ready()
    {
		StopwatchComponent.Timeout += Fade;
		StopwatchComponent.Tap(ExistDuration, () => {
			QueueFree();
		});

		InventoryComponent.PopulateFromData(LootRatios);
    }

    public override void _Process(double delta)
    {
		LabelPivot.Rotation = -Rotation;
    }
	
    private void Fade()
	{
		Modulate = new Color(Modulate.R, Modulate.G, Modulate.B, Modulate.A - (1f / ExistDuration));
	}

    public void ShowInteractText()
	{
		LabelPivot.Visible = true;
	}

    public void HideInteractText()
	{
		LabelPivot.Visible = false;
	}

    public void Interact(IEntity interactor)
    {
		InventoryComponent.TransferTo(interactor.InventoryComponent);
		interactor.InventoryComponent.PlaySound();
		QueueFree();
    }

    public bool CanInteract() => !StopwatchComponent.IsStopped();

}

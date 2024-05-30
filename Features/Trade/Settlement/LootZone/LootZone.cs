using Godot;
using System;

public partial class LootZone : StaticBody2D, IInteractable
{
	[Export]
	public Label LootLabel { get; set; }

	[Export]
	public InventoryComponent InventoryComponent { get; set; }

	[Export]
	public LootRatioResource LootRatios { get; set; }

    public override void _Ready()
    {
        base._Ready();
		
		InventoryComponent.PopulateFromData(LootRatios);
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void Interact(IEntity interactor)
    {
		InventoryComponent.TransferTo(interactor.InventoryComponent);
		QueueFree();
    }

    public bool CanInteract() => Visible;

    public void ShowInteractText()
    {
		LootLabel.Visible = true;
    }

    public void HideInteractText()
    {
		LootLabel.Visible = false;
    }
}

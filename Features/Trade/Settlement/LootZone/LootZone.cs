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

  private bool wasLooted = false;
    public override void _Ready()
    {
        base._Ready();
        Reset();
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void Interact(IEntity interactor)
    {
      wasLooted = true;
      InventoryComponent.TransferTo(interactor.InventoryComponent);
      interactor.InventoryComponent.PlaySound();
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

    public bool WasLooted() => wasLooted;

    public void Reset()
    {
		  InventoryComponent.PopulateFromData(LootRatios);
      wasLooted = false;
    }
}

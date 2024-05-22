using Godot;
using System;

public partial class InventoryDisplay : Control
{
	[Export]
	public Player Player { get; set; }

	[Export]
	public Label CannonballLabel { get; set; }

	[Export]
	public Label FoodLabel { get; set; }

	[Export]
	public Label WoodLabel { get; set; }

	[Export]
	public Label MoneyLabel { get; set; }

    public override void _Ready()
    {
		Player.InventoryComponent.InventoryChanged += onInventoryChanged;
    }

	private void onInventoryChanged(int cannonballs, int food, int wood, int gold)
	{
		CannonballLabel.Text = cannonballs.ToString();
		FoodLabel.Text = food.ToString();
		WoodLabel.Text = wood.ToString();
		MoneyLabel.Text = "$" + gold.ToString();
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("ToggleInventory"))
		{
			Visible = !Visible;
		}
    }
}

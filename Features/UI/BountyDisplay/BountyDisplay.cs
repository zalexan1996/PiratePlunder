using Godot;
using System;
using System.Diagnostics;

public partial class BountyDisplay : PanelContainer
{
	[Export]
	public Label BountyLabel { get; set; }

	private BountyService BountyService => GetTree().GetAutoLoad().BountyService;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BountyService.BountyUpdated += onBountyUpdated;
		BountyLabel.Text = String.Format("{0:C0}", 0);
		
	}

	private void onBountyUpdated(int newBounty)
	{
		if (BountyLabel is not null)
		{
			BountyLabel.Text = String.Format("{0:C0}", newBounty);
		}
	}

    public override void _ExitTree()
    {
		BountyService.BountyUpdated -= onBountyUpdated;
        base._ExitTree();
    }
}

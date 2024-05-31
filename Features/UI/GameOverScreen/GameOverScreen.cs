using Godot;
using System;

public partial class GameOverScreen : PanelContainer
{
	[Export]
	public Label InsultLabel { get; set; }

	[Export]
	public Label BountyLabel { get; set; }

	[Export]
	public Label HighestBountyLabel { get; set; }

	private BountyService BountyService => GetTree().GetAutoLoad().BountyService;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BountyService.SaveBounty();
		Reset();
	}

	public void Reset()
	{
		var insult = GetTree().GetAutoLoad().InsultService.GetInsult();
		InsultLabel.Text = insult;
		BountyLabel.Text = String.Format("{0:C0}", BountyService.Bounty);
		HighestBountyLabel.Text = String.Format("{0:C0}", BountyService.GetHighestBounty());
	}

	private void onPlay()
	{
		GetTree().ReloadCurrentScene();
	}

	private void onQuit()
	{
		GetTree().Quit();
	}
}

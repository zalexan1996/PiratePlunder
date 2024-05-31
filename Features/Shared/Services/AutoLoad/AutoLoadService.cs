using Godot;
using System;

public static class AutoLoadExtensions
{
	public static IAutoLoadService GetAutoLoad(this SceneTree tree)
	{
		return (IAutoLoadService)tree.CurrentScene.GetNode("/root/AutoLoadService");
	}
}

public interface IAutoLoadService
{
	public SpawnerService SpawnerService { get; set; }
	public BountyService BountyService { get; set; }
	public InsultService InsultService { get; set; }
}
public partial class AutoLoadService : Node2D, IAutoLoadService
{
	[Export]
	public SpawnerService SpawnerService { get; set; }

	[Export]
	public BountyService BountyService { get; set; }

	[Export]
	public InsultService InsultService { get; set; }
}

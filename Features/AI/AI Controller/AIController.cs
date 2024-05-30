using Godot;
using System;

public partial class AIController : Node
{
	[Export]
	public Enemy Controls { get; set; } = null;
    public IEntity Target { get; set; } =  null;


    public override void _Ready()
    {
        SetPhysicsProcess(false);
        NavigationServer2D.MapChanged += SyncWithNavigationServer;
    }

    private void SyncWithNavigationServer(Rid map)
    {
        SetPhysicsProcess(true);
    }
}

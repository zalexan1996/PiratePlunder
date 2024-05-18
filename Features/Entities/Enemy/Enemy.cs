using System;
using System.Diagnostics;
using Godot;

public partial class Enemy : Node2D
{
    [Export]
    public Ship Ship { get; set; }

    [Export]
    public NavigationAgent2D NavigationAgent2D { get; set; }

    [Export]
    public Node2D FollowTarget { get; set; }
    private float navUpdateDelay = 0.0f;
    public override void _Ready()
    {
        SetPhysicsProcess(false);
        NavigationServer2D.MapChanged += SyncWithNavigationServer;
    }

    public void SyncWithNavigationServer(Rid map)
    {
        var rng = new RandomNumberGenerator();
        navUpdateDelay = rng.RandiRange(1, 100);
        SetPhysicsProcess(true);
        NavigationAgent2D.TargetPosition = FollowTarget.GlobalPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (FollowTarget is null)
        {
            return;
        }

        if ((NavigationAgent2D.TargetPosition - FollowTarget.GlobalPosition).Length() > 100 && navUpdateDelay-- <= 0)
        {
            NavigationAgent2D.TargetPosition = FollowTarget.GlobalPosition;
            var rng = new RandomNumberGenerator();
            navUpdateDelay = rng.RandiRange(1, 100);
        }

        var nextPosition = NavigationAgent2D.GetNextPathPosition();
        Ship.InputVector = Ship.GlobalPosition.DirectionTo(nextPosition);
    }
}

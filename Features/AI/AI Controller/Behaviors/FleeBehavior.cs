using System.Diagnostics;
using Godot;

public class FleeBehavior : IBehavior
{
    public Enemy Controls { get; set; }
    public IEntity FleeFrom { get; set; }

    private int navTimer = 0;
    public void Execute()
    {
        if (FleeFrom is not null && navTimer++ > 20)
        {
            Controls.NavigationAgent2D.TargetPosition = Controls.GlobalPosition + (Controls.GlobalPosition - FleeFrom.GlobalPosition).Normalized() * 10000;

            var nextPosition = Controls.NavigationAgent2D.GetNextPathPosition();
            Controls.InputVector = Controls.Ship.GlobalPosition.DirectionTo(nextPosition);
            Controls.Ship.AimLocation = nextPosition;
        }
    }

	public void StartFlee(IEntity entity)
	{
        if (FleeFrom is null)
        {
            FleeFrom = entity;
        }
	}

	public void StopFlee(IEntity entity)
	{
        if (entity == FleeFrom)
        {
            FleeFrom = null;
            Controls.InputVector = Vector2.Zero;

            var nextFleeTarget = Controls.ProximityDetectionComponent.GetNextHostileEntityInProximity();
            if (nextFleeTarget is not null)
            {
                StartFlee(nextFleeTarget);
            }
        }
	}
}
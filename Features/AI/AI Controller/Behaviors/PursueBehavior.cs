using Godot;

public class PursueBehavior : IBehavior
{
    public Enemy Controls { get; set; }
    public IEntity Target { get; set; }

    private float navUpdateDelay = 0.0f;
    public void Execute()
    {
        if (Controls is null || Target is null)
        {
            return;
        }
        
        if ((Controls.NavigationAgent2D.TargetPosition - Target.GlobalPosition).Length() > 100 && navUpdateDelay-- <= 0)
        {
            Controls.NavigationAgent2D.TargetPosition = Target.GlobalPosition;
            var rng = new RandomNumberGenerator();
            navUpdateDelay = rng.RandiRange(1, 100);
        }

        var nextPosition = Controls.NavigationAgent2D.GetNextPathPosition();
        Controls.InputVector = Controls.Ship.GlobalPosition.DirectionTo(nextPosition);
        Controls.Ship.AimLocation = Target.GlobalPosition;
        
        if (Target is not null && Controls.Ship.Cannon.CanFire())
        {
            Controls.Ship.Fire();
        }
    }

	public void StartFollow(IEntity entity)
	{
        if (Target is null)
        {
            Target = entity;
        }
	}

	public void StopFollow(IEntity entity)
	{
        if (entity == Target)
        {
            Target = null;
            Controls.InputVector = Vector2.Zero;

            var nextFollowTarget = Controls.ProximityDetectionComponent.GetNextEntityInProximity();

            if (nextFollowTarget is not null)
            {
                StartFollow(nextFollowTarget);
            }
        }
	}
}
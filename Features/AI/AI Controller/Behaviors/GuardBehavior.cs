using Godot;

public class GuardBehavior : IBehavior
{
    public delegate void TargetReached(GuardBehavior behavior);
    public event TargetReached TargetReachedEventHandler;
    public Enemy Controls { get; set; }
    public Node2D Guards { get; set; }
    public float Range { get; set; } = 2000f;
    private Vector2 target = Vector2.Zero;
    private float navUpdateDelay = 0.0f;
    private int tickCounter = 0;
    public void Reset()
    {
        tickCounter = 0;
        var rng = new RandomNumberGenerator();
        target = Guards.GlobalPosition + new Vector2(rng.RandfRange(-1,1), rng.RandfRange(-1,1)).Normalized() * Range * rng.RandfRange(0.25f, 1f);
    }

    public void Execute()
    {
        if (Controls is null || Guards is null)
        {
            return;
        }

        if (target == Vector2.Zero)
        {
            Reset();
        }
        
        if ((Controls.NavigationAgent2D.TargetPosition - target).Length() > 100 && navUpdateDelay-- <= 0)
        {
            Controls.NavigationAgent2D.TargetPosition = target;
            var rng = new RandomNumberGenerator();
            navUpdateDelay = rng.RandiRange(1, 100);
        }

        var nextPosition = Controls.NavigationAgent2D.GetNextPathPosition();
        Controls.InputVector = Controls.Ship.GlobalPosition.DirectionTo(nextPosition);
        Controls.Ship.AimLocation = target;

        if (Controls.Ship.GlobalPosition.DistanceTo(target) < 100 || tickCounter++ > 300)
        {
            TargetReachedEventHandler?.Invoke(this);
            Reset();
        }
    }
}
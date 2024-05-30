using Godot;

public class EnterTradeRouteBehavior : IBehavior
{
    public delegate void TradeRouteReachedEventHandler(EnterTradeRouteBehavior behavior);
    public event TradeRouteReachedEventHandler TradeRouteReached;

    public Enemy Controls { get; set; }
    public TradeRoute TradeRoute { get; set; }
    public TradeRoutePoint TargetPoint { get; set; } = null;
    
    public void Reset()
    {
            TargetPoint = TradeRoute.GetNearestTradeRoutePoint(Controls.GlobalPosition);
            Controls.NavigationAgent2D.TargetPosition = TargetPoint.GlobalPosition;
    }

    public void Execute()
    {
        if (Controls is null || TradeRoute is null)
        {
            return;
        }

        if (TargetPoint is null)
        {
            Reset();
        }
        

        var nextPosition = Controls.NavigationAgent2D.GetNextPathPosition();
        Controls.InputVector = Controls.Ship.GlobalPosition.DirectionTo(nextPosition);
        Controls.Ship.AimLocation = nextPosition;

        if (TargetPoint.GlobalPosition.DistanceTo(Controls.GlobalPosition) < 50)
        {
            TradeRouteReached?.Invoke(this);
        }   
    }
}
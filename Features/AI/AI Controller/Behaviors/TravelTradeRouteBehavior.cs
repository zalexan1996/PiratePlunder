using System.Diagnostics;
using Godot;

public class TravelTradeRouteBehavior : IBehavior
{
    public delegate void TradeRoutePointReachedEventHandler(TravelTradeRouteBehavior behavior);
    public event TradeRoutePointReachedEventHandler TradeRoutePointReached;

    public Enemy Controls { get; set; }
    public TradeRoute TradeRoute { get; set; }
    public TradeRoutePoint PreviouslyVisitedPoint { get; set; }
    public TradeRoutePoint CurrentPoint { get; set; }
    
    public void Execute()
    {
        if (Controls is null || TradeRoute is null)
        {
            return;
        }
        
        if (CurrentPoint.GlobalPosition != Controls.NavigationAgent2D.TargetPosition)
        {
            Controls.NavigationAgent2D.TargetPosition = CurrentPoint.GlobalPosition;
        }
        
        travel();

        if (CurrentPoint.GlobalPosition.DistanceTo(Controls.GlobalPosition) <= 50)
        {
            TradeRoutePointReached?.Invoke(this);
        }   
    }

    private void travel()
    {
        var nextPosition = Controls.NavigationAgent2D.GetNextPathPosition();
        Controls.InputVector = Controls.Ship.GlobalPosition.DirectionTo(nextPosition);
        Controls.Ship.AimLocation = nextPosition;
    }
}
using Godot;
using System;
using System.Diagnostics;
using System.Linq;

/*
Merchant AI rules

If there is a 
*/
public partial class MerchantAiController : AIController
{
	private FleeBehavior fleeBehavior = null;
    private EnterTradeRouteBehavior enterTradeRouteBehavior = null;
    private TravelTradeRouteBehavior travelTradeRouteBehavior = null;

    private bool isFleeing = false;
    private bool isEnteringTradeRoute = true;
    private bool isTravelingTradeRoute = false;

    public override void _Ready()
    {
        base._Ready();

        Controls.ProximityDetectionComponent.EntityDetected += startFlee;
        Controls.ProximityDetectionComponent.EntityLeft += stopFlee;

		fleeBehavior = new FleeBehavior()
		{
			Controls = Controls,
			FleeFrom = Target
		};

        enterTradeRouteBehavior = new EnterTradeRouteBehavior()
        {
            Controls = Controls,
            TradeRoute = GetTree().CurrentScene.GetClosestChild<TradeRoute>(Controls)
        };
        enterTradeRouteBehavior.TradeRouteReached += onJoinedTradeRoute;

        travelTradeRouteBehavior = new TravelTradeRouteBehavior()
        {
            Controls = Controls,
            TradeRoute = enterTradeRouteBehavior.TradeRoute
        };
        travelTradeRouteBehavior.TradeRoutePointReached += onTradeRoutePointReached;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (isFleeing)
        {
		    fleeBehavior.Execute();
        }
        else if (isEnteringTradeRoute)
        {
            enterTradeRouteBehavior.Execute();
        }
        else if (isTravelingTradeRoute)
        {
            travelTradeRouteBehavior.Execute();
        }
    }

    private void startFlee(IEntity entity)
    {
        Debug.WriteLine("Start Flee");
        fleeBehavior.StartFlee(entity);
        isFleeing = true;
        isEnteringTradeRoute = false;
    }

    private void stopFlee(IEntity entity)
    {
        Debug.WriteLine("Stop Flee");
        fleeBehavior.StopFlee(entity);
        enterTradeRouteBehavior.Reset();
        isFleeing = false;
        isEnteringTradeRoute = true;
    }

    private void onJoinedTradeRoute(EnterTradeRouteBehavior behavior)
    {
        travelTradeRouteBehavior.TradeRoute = behavior.TradeRoute;
        travelTradeRouteBehavior.PreviouslyVisitedPoint = behavior.TargetPoint;
        travelTradeRouteBehavior.CurrentPoint = behavior.TradeRoute.GetNextTradeRoutePoint(behavior.TargetPoint);
        isEnteringTradeRoute = false;
        isTravelingTradeRoute = true;


        Debug.WriteLine($"Joined Trade route: {travelTradeRouteBehavior.PreviouslyVisitedPoint.GlobalPosition} | {travelTradeRouteBehavior.CurrentPoint.GlobalPosition}");
    }

    private void onTradeRoutePointReached(TravelTradeRouteBehavior behavior)
    {
        Debug.WriteLine("Traveling Trade Route");
        travelTradeRouteBehavior.PreviouslyVisitedPoint = behavior.CurrentPoint;
        travelTradeRouteBehavior.CurrentPoint = behavior.TradeRoute.GetNextTradeRoutePoint(behavior.CurrentPoint);
    }
}
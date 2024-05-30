using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class TradeRoute : Node2D
{
	[Export]
	public FactionResource Faction { get; set; }
	public IList<TradeRoutePoint> Points { get; set; } = new List<TradeRoutePoint>();

    public override void _Ready()
    {
		Points = GetChildren()
			.Where(c => c.GetType() == typeof(TradeRoutePoint))
			.Select(c => (TradeRoutePoint)c)
			.ToList();
    }

	public TradeRoutePoint GetNearestTradeRoutePoint(Vector2 startingLocation)
	{
		return Points.MinBy(p => startingLocation.DistanceTo(p.GlobalPosition));
	}

	public TradeRoutePoint GetNextTradeRoutePoint(TradeRoutePoint previous)
	{
		return Points.Next(previous);
	}
}

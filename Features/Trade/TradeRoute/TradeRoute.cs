using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class TradeRoute : Node2D
{
	[Export]
	public FactionResource Faction { get; set; }
	public IList<TradeRoutePoint> Points { get; set; } = new List<TradeRoutePoint>();

    public override void _Ready()
    {
		Points = this.SearchForChildren<TradeRoutePoint>()
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

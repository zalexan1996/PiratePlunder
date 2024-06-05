using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Settlement : TradeRoutePoint
{
	[Export]
	public FactionResource FactionResource { get; set; }

	[Export]
	public Array<GroundCannon> Cannons { get; set; }

	[Export]
	public Timer FireTimer { get; set; }

	[Export]
	public Area2D InteractionArea { get; set; }

	[Export]
	public LootZone LootZone { get; set; }

	[Export]
	public Timer RespawnTimer { get; set; }

	[Export]
	public ShipData MerchantShipData { get; set; }

	[Export]
	public ShipData NavyShipData { get; set; }

	private IList<IEntity> Targets = new List<IEntity>();

    public override void _Ready()
    {
		foreach(var cannon in Cannons)
		{
			cannon.FactionResource = FactionResource;
			cannon.Destroyed += onCannonDestroyed;
		}

		RespawnTimer.Start();
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

		if (!Targets.Any())
		{
			return;
		}

		foreach (var cannon in Cannons)
		{
			cannon.Rotation = (Targets.First().GlobalPosition - cannon.GlobalPosition).Rotated(-Rotation).Angle() - Mathf.Pi / 2;
		}
    }

    private void onEntityOverlap(Node2D body)
	{
		var entity = body as IEntity;
		if (entity is null)
		{
			return;
		}


		if (!entity.IsInFaction(FactionResource) && Cannons.Count > 0)
		{
			if (!Targets.Contains(entity))
			{
				Targets = Targets.Append(entity).ToList();
			}

			FireTimer.Start(1 / Cannons.Count);
		}
	}

	private void onEntityEndOverlap(Node2D body)
	{
		var entity = body as IEntity;
		if (entity is null)
		{
			return;
		}

		if (Targets.Contains(entity))
		{
			Targets.Remove(entity);
		}

		if (!Targets.Any())
		{
			FireTimer.Stop();
		}
	}

	private void onFireTimerTimeout()
	{
		var availableCannons = Cannons.Where(c => c.Visible).ToList();
		if (!Targets.Any() || !availableCannons.Any())
		{
			return;
		}

		var rng = new RandomNumberGenerator();
		int i = rng.RandiRange(0, availableCannons.Count() - 1);
		
		var randomCannon = availableCannons[i];

		randomCannon.Cannon.Fire(Targets.First().GlobalPosition, Vector2.Zero, randomCannon);
		FireTimer.Start( 1 / Cannons.Count);
	}

	private void onCannonDestroyed(GroundCannon cannon)
	{
		if (!Cannons.Any(c => c.Visible))
		{
			MakeLootable();
		}
	}

	private void onRespawn()
	{
		// Regen a destroyed cannon
		var destroyedCannon = Cannons.FirstOrDefault(c => !c.Visible);
		if (destroyedCannon is not null)
		{
			destroyedCannon.Reset();
		}
		// Spawn a ship
		else
		{
			if (LootZone.WasLooted())
			{
				LootZone.Reset();
			}

			var rng = new RandomNumberGenerator();
			if (rng.RandiRange(0, 10) < 5)
			{
				GetTree().GetAutoLoad().SpawnerService.SpawnEnemy(LootZone.GlobalPosition, MerchantShipData);
			}
			else
			{
				var enemy =GetTree().GetAutoLoad().SpawnerService.SpawnEnemy(LootZone.GlobalPosition, NavyShipData);
				(enemy.AIController as ViolentAiController).guardBehavior.Guards = this;
			}
		}
		

		
		RespawnTimer.Start();
	}

	public void MakeLootable()
	{
		LootZone.Rotation = -Rotation;
		LootZone.Visible = true;

		RespawnTimer.Stop();
		RespawnTimer.Start();

		GetTree().GetAutoLoad().BountyService.SettlementLooted();
	}
}

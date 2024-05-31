using Godot;
using Godot.Collections;
using System;
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

    public override void _Ready()
    {
		foreach(var cannon in Cannons)
		{
			cannon.FactionResource = FactionResource;
			cannon.Destroyed += onCannonDestroyed;
		}

		RespawnTimer.Start();
    }

    private IEntity Target = null;

    public override void _Process(double delta)
    {
        base._Process(delta);

		if (Target is null)
		{
			return;
		}

		foreach (var cannon in Cannons)
		{
			cannon.Rotation = (Target.GlobalPosition - cannon.GlobalPosition).Rotated(-Rotation).Angle() - Mathf.Pi / 2;
		}
    }

    private void onEntityOverlap(Node2D body)
	{
		var entity = body as IEntity;
		if (entity is not null && !entity.IsInFaction(FactionResource) && Cannons.Count > 0)
		{
			Target = entity;
			FireTimer.Start(1 / Cannons.Count);
		}
	}

	private void onEntityEndOverlap(Node2D body)
	{
		var entity = body as IEntity;
		if (entity is not null && entity == Target)
		{
			Target = null;
			FireTimer.Stop();
		}
	}

	private void onFireTimerTimeout()
	{
		var availableCannons = Cannons.Where(c => c.Visible).ToList();
		if (Target is null || !availableCannons.Any())
		{
			return;
		}

		var rng = new RandomNumberGenerator();
		int i = rng.RandiRange(0, availableCannons.Count() - 1);
		
		var randomCannon = availableCannons[i];

		randomCannon.Cannon.Fire(Target.GlobalPosition, Vector2.Zero, randomCannon);
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
			if (rng.RandiRange(0, 10) > 8)
			{
				GetTree().GetAutoLoad().SpawnerService.SpawnEnemy(LootZone.GlobalPosition, MerchantShipData);
			}
			else
			{
				GetTree().GetAutoLoad().SpawnerService.SpawnEnemy(LootZone.GlobalPosition, NavyShipData);
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

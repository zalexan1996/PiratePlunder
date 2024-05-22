using System;
using System.Diagnostics;
using Godot;

public partial class Enemy : CharacterBody2D, IEntity
{
    [Export]
    public ShipData ShipData { get; set; }

	[Export]
	public RotationalAlignerComponent RotationalAlignerComponent { get; set; }
	private IRotationalAlignerComponent _RotationalAlignerComponent => RotationalAlignerComponent;

    [Export]
    public FactionComponent FactionComponent { get; set; }

	[Export]
	public Vector2 InputVector { get; set; } = Vector2.Zero;

    [Export]
    public Ship Ship { get; set; }

	[Export]
	public HealthComponent HealthComponent { get; set; }

    [Export]
    public NavigationAgent2D NavigationAgent2D { get; set; }

	[Export]
	public Node2D HealthDisplayPoint { get; set; }

    public IEntity FollowTarget { get; set; }

	[Export]
	public VelocityComponent VelocityComponent { get; set; }

	private IVelocityComponent VelocityInterface => VelocityComponent;

    [Export]
    public ProximityDetectionComponent ProximityDetectionComponent { get; set; }

    [Export]
    public InventoryComponent InventoryComponent { get; set; }
    private bool shouldFollowPlayer = false;
    private float navUpdateDelay = 0.0f;
    public override void _Ready()
    {
        VelocityComponent.MaxSpeed = ShipData.MaxSpeed;
        Ship.Cannon.ReloadDelay = ShipData.ReloadDuration;
        HealthComponent.MaxHealth = ShipData.MaxHealth;
        FactionComponent.Faction = ShipData.FactionResource;
        Ship.GetNode<Sprite2D>("Sprite").Texture = ShipData.BoatImage;
        
        SetPhysicsProcess(false);
        NavigationServer2D.MapChanged += SyncWithNavigationServer;
        RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
		};
		Ship.CannonAligner.RotationHandler = () => {
			return (Ship.AimLocation - GlobalPosition).Rotated(-Rotation);
		};

        ProximityDetectionComponent.EntityOwner = this;
        ProximityDetectionComponent.EntityDetected += startFollow;
        ProximityDetectionComponent.EntityLeft += stopFollow;
    }

    public void SyncWithNavigationServer(Rid map)
    {
        var rng = new RandomNumberGenerator();
        navUpdateDelay = rng.RandiRange(1, 100);
        SetPhysicsProcess(true);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		Vector2 direction = InputVector;

		if (!direction.IsZeroApprox())
		{
			VelocityInterface.ApplyInputVector(direction);
		}

		Velocity = VelocityInterface.CurrentVelocity;
		MoveAndSlide();

        if (FollowTarget is not null && Ship is not null && shouldFollowPlayer)
        {
            followPlayer();
        }

        Ship.SetTrailIntensity(Mathf.Lerp(0, 1, Velocity.Length() / VelocityComponent.MaxSpeed));
        HealthDisplayPoint.Rotation = -Rotation;
    }

    private void startFollow(IEntity entity)
    {
        if (FollowTarget is null && ShipData.ShipType.ShipTypeId != "MERCHANT")
        {
            FollowTarget = entity;
            shouldFollowPlayer = true;
        }
    }

    private void stopFollow(IEntity entity)
    {
        if (entity == FollowTarget)
        {
            FollowTarget = null;
            shouldFollowPlayer = false;
            InputVector = Vector2.Zero;

            var nextFollowTarget = ProximityDetectionComponent.GetNextEntityInProximity();

            if (nextFollowTarget is not null)
            {
                startFollow(nextFollowTarget);
            }
        }

    }

    private void followPlayer()
    {
        if ((NavigationAgent2D.TargetPosition - FollowTarget.GlobalPosition).Length() > 100 && navUpdateDelay-- <= 0)
        {
            NavigationAgent2D.TargetPosition = FollowTarget.GlobalPosition;
            var rng = new RandomNumberGenerator();
            navUpdateDelay = rng.RandiRange(1, 100);
        }

        var nextPosition = NavigationAgent2D.GetNextPathPosition();
        InputVector = Ship.GlobalPosition.DirectionTo(nextPosition);
        Ship.AimLocation = FollowTarget.GlobalPosition;

        if (Ship.Cannon.CanFire())
        {
            Ship.Fire();
        }
    }

    public void TakeDamage(int damage)
    {
		HealthComponent.TakeDamage(1);
        if (HealthComponent.IsDead())
        {
            GetTree().GetAutoLoad().SpawnerService.SpawnWreckage(GlobalPosition, Rotation, ShipData.ShipType.LootRatios);
            QueueFree();
        }

        if (HealthComponent.CurrentHealth / (float)HealthComponent.MaxHealth <= 0.5 && ShipData.DamagedBoatImage is not null)
        {
            Ship.GetNode<Sprite2D>("Sprite").Texture = ShipData.DamagedBoatImage;
        }
    }

    public FactionResource GetFaction() => FactionComponent.Faction;

    public bool IsHostileWith(IEntity entity)
    {
        return !entity.IsInFaction(GetFaction());
    }

}

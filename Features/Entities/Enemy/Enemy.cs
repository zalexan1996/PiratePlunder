using System;
using System.Diagnostics;
using Godot;

public partial class Enemy : CharacterBody2D, IEntity, IHasMouseOverDisplay
{
    [Export]
    public ShipData ShipData { get; set; }

    public AIController AIController { get; set; }

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


	[Export]
	public VelocityComponent VelocityComponent { get; set; }

	private IVelocityComponent VelocityInterface => VelocityComponent;

    [Export]
    public ProximityDetectionComponent ProximityDetectionComponent { get; set; }

    [Export]
    public InventoryComponent InventoryComponent { get; set; }

    [Export]
    public EnemyInfoWidget EnemyInfoWidget { get; set; }

    public override void _Ready()
    {
        VelocityComponent.MaxSpeed = ShipData.MaxSpeed;
        Ship.Cannon.ReloadDelay = ShipData.ReloadDuration;
        HealthComponent.MaxHealth = ShipData.MaxHealth;
        FactionComponent.Faction = ShipData.FactionResource;
        Ship.GetNode<Sprite2D>("Sprite").Texture = ShipData.BoatImage;
        
        RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
		};
		Ship.CannonAligner.RotationHandler = () => {
			return (Ship.AimLocation - GlobalPosition).Rotated(-Rotation);
		};

        ProximityDetectionComponent.EntityOwner = this;

        AIController = ShipData.ShipType.AIControllerScene.Instantiate<AIController>();
        AIController.Controls = this;
        AddChild(AIController);
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


        Ship.SetTrailIntensity(Mathf.Lerp(0, 1, Velocity.Length() / VelocityComponent.MaxSpeed));
        HealthDisplayPoint.Rotation = -Rotation;
    }



    public void TakeDamage(int damage, IEntity dealer)
    {
		HealthComponent.TakeDamage(1);
        if (HealthComponent.IsDead())
        {
            GetTree().GetAutoLoad().SpawnerService.SpawnWreckage(GlobalPosition, Rotation, ShipData.ShipType.LootRatios);
            GetTree().GetAutoLoad().SpawnerService.SpawnShockwave(GlobalPosition);
            GetTree().GetAutoLoad().BountyService.ShipDestroyed(ShipData, dealer);
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

    public void ShowMouseOverDisplay()
    {
        EnemyInfoWidget.Show();
    }

    public void HideMouseOverDisplay()
    {
        // EnemyInfoWidget.Visible = FollowTarget is not null;
    }
}
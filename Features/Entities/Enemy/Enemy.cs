using System;
using System.Diagnostics;
using Godot;

public partial class Enemy : CharacterBody2D, ITakesDamage
{
	[Export]
	public RotationalAlignerComponent RotationalAlignerComponent { get; set; }
	private IRotationalAlignerComponent _RotationalAlignerComponent => RotationalAlignerComponent;

	[Export]
	public Vector2 InputVector { get; set; } = Vector2.Zero;
    [Export]
    public Ship Ship { get; set; }

	[Export]
	public HealthComponent HealthComponent { get; set; }
    [Export]
    public NavigationAgent2D NavigationAgent2D { get; set; }

    [Export]
    public Node2D FollowTarget { get; set; }

	[Export]
	public VelocityComponent VelocityComponent { get; set; }
	private IVelocityComponent VelocityInterface => VelocityComponent;
    [Export]
    public ProximityDetectionComponent ProximityDetectionComponent { get; set; }

    private bool shouldFollowPlayer = false;
    private float navUpdateDelay = 0.0f;
    public override void _Ready()
    {
        SetPhysicsProcess(false);
        NavigationServer2D.MapChanged += SyncWithNavigationServer;
        RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
		};
		Ship.CannonAligner.RotationHandler = () => {
			return (Ship.AimLocation - GlobalPosition).Rotated(-Rotation);
		};
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
    }

    private void startFollow(Player player)
    {
        FollowTarget = player;
        shouldFollowPlayer = true;
        Debug.WriteLine("Following");
    }

    private void stopFollow(Player player)
    {
        FollowTarget = null;
        shouldFollowPlayer = false;
        InputVector = Vector2.Zero;
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
            QueueFree();
        }
    }
}

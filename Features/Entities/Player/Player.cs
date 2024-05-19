
using Godot;

public partial class Player : CharacterBody2D, ITakesDamage
{
    [Export]
    public ShipData ShipData { get; set; }
	[Export]
	public RotationalAlignerComponent RotationalAlignerComponent { get; set; }
	private IRotationalAlignerComponent _RotationalAlignerComponent => RotationalAlignerComponent;

	[Export]
	public Vector2 InputVector { get; set; } = Vector2.Zero;
	[Export]
	public HealthComponent HealthComponent { get; set; }
    [Export]
    public PlayerInputComponent PlayerInputComponent { get; set; }
    private IPlayerInputComponent _PlayerInputComponent => PlayerInputComponent;

    [Export]
    public Ship Ship { get; set; }

	[Export]
	public VelocityComponent VelocityComponent { get; set; }
	private IVelocityComponent VelocityInterface => VelocityComponent;
    public override void _Ready()
    {
        VelocityComponent.MaxSpeed = ShipData.MaxSpeed;
        Ship.Cannon.ReloadDelay = ShipData.ReloadDuration;
        HealthComponent.MaxHealth = ShipData.MaxHealth;
        PlayerInputComponent.Fire += onFire;
        RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
		};
        
		Ship.CannonAligner.RotationHandler = () => {
			return (Ship.AimLocation - GlobalPosition).Rotated(-Rotation);
		};
    }

    public override void _PhysicsProcess(double delta)
    {
		Vector2 direction = InputVector;

		if (!direction.IsZeroApprox())
		{
			VelocityInterface.ApplyInputVector(direction);
		}

		Velocity = VelocityInterface.CurrentVelocity;
		MoveAndSlide();

        InputVector = _PlayerInputComponent.InputVector;
        Ship.AimLocation = GetGlobalMousePosition();
        Ship.SetTrailIntensity(Mathf.Lerp(0, 1, Velocity.Length() / VelocityComponent.MaxSpeed));

        base._PhysicsProcess(delta);
    }

    private void onFire()
    {
        Ship.Fire();
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
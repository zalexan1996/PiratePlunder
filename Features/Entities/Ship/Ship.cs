using Godot;
using System;
using System.Diagnostics;

public partial class Ship : CharacterBody2D, ITakesDamage
{
	[Export]
	public RotationalAlignerComponent RotationalAlignerComponent { get; set; }
	private IRotationalAlignerComponent _RotationalAlignerComponent => RotationalAlignerComponent;

	[Export]
	public VelocityComponent VelocityComponent { get; set; }
	private IVelocityComponent VelocityInterface => VelocityComponent;

	[Export]
	public HealthComponent HealthComponent { get; set; }

	[Export]
	public AnimatedSprite2D Trail { get; set; }

	[Export]
	public Vector2 InputVector { get; set; } = Vector2.Zero;

	[Export]
	public RotationalAlignerComponent CannonAligner { get; set; }

	[Export]
	public Vector2 AimLocation { get; set; }

	[Export]
	public Cannon Cannon { get; set; }

    public override void _Ready()
    {
        base._Ready();
		_RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
		};

		CannonAligner.RotationHandler = () => {
			return (AimLocation - GlobalPosition).Rotated(-Rotation);
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
		
		Trail.Modulate = new Color(1,1,1,Mathf.Lerp(0, 1, Velocity.Length() / VelocityComponent.MaxSpeed));
	}

    public void TakeDamage(int damage)
    {
		HealthComponent.TakeDamage(1);
        if (HealthComponent.IsDead())
        {
            QueueFree();
        }
    }

	public void Fire()
	{
		Cannon.Fire(AimLocation, Vector2.Zero, this);
	}
}

using Godot;
using System;

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
	public Vector2 InputVector { get; set; } = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();
		_RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
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

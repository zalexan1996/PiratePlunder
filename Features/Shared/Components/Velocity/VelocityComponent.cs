using System.Diagnostics;
using Godot;

public interface IVelocityComponent
{
	float Dampening { get; set; }
	float Acceleration { get; set; }
	float MaxSpeed { get; set; }
	public Vector2 CurrentVelocity { get; }
	void ApplyVelocity(Vector2 additionalVelocity);
	public void ApplyInputVector(Vector2 inputVector);

}
public partial class VelocityComponent : Node, IVelocityComponent
{
	[Export]
	public float Dampening { get; set; } = 1.0f;

	[Export]
	public float Acceleration { get; set; } = 10.0f;

	[Export]
	public float MaxSpeed { get; set; } = 250.0f;

	public Vector2 CurrentVelocity { get; protected set; } = Vector2.Zero;
	private bool velocityApplied = false;

	public void ApplyVelocity(Vector2 additionalVelocity)
	{
		CurrentVelocity += additionalVelocity;
		velocityApplied = true;
	}


	public void ApplyInputVector(Vector2 inputVector)
	{
		CurrentVelocity += inputVector.Normalized() * Acceleration;
		velocityApplied = true;
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

			CurrentVelocity = CurrentVelocity.MoveToward(Vector2.Zero, Dampening);

		if (CurrentVelocity.Length() > MaxSpeed)
		{
			CurrentVelocity = CurrentVelocity.Normalized() * MaxSpeed;
		}
		
		velocityApplied = false;
    }
}

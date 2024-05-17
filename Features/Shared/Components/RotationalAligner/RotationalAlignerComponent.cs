using Godot;
using System;

public interface IRotationalAlignerComponent
{
	public Node2D Target { get; set; }
	public Func<Vector2> RotationHandler { get; set; }
}
public partial class RotationalAlignerComponent : Node, IRotationalAlignerComponent
{
	[Export]
	public Node2D Target { get; set; }

	public Func<Vector2> RotationHandler { get; set; }

    public override void _PhysicsProcess(double delta)
    {
		if (RotationHandler is null)
		{
			return;
		}

		var rotation = RotationHandler();

		if (rotation.IsZeroApprox())
		{
			return;
		}
		
		Target.Rotation = rotation.Angle() - (float)Math.PI/2;
		
    }
}

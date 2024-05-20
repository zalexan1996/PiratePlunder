using Godot;
using System;
using System.Diagnostics;

public partial class Ship : Node2D
{
	[Export]
	public AnimatedSprite2D Trail { get; set; }

	[Export]
	public RotationalAlignerComponent CannonAligner { get; set; }

	[Export]
	public Vector2 AimLocation { get; set; }

	[Export]
	public Cannon Cannon { get; set; }

    public override void _Ready()
    {
        base._Ready();
    }

	public void Fire()
	{
		Cannon.Fire(AimLocation, Vector2.Zero, (Node2D)GetParent());
	}

	public void SetTrailIntensity(float intensity)
	{
		Trail.Modulate = new Color(1,1,1,intensity);
	}
}

using Godot;
using System;
using System.Diagnostics;

public partial class Explosion : Node2D
{
	[Export]
	public AnimatedSprite2D AnimatedSprite2D { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimatedSprite2D.AnimationFinished += onAnimationFinished;
	}

	private void onAnimationFinished()
	{
		QueueFree();
	}

	private void onBodyOverlap(Node2D body)
	{
		Debug.WriteLine($"Colliding with: {body.Name}");
		
		var damageableBody = body as ITakesDamage;
		if (damageableBody == null)
		{
			return;
		}

		damageableBody.TakeDamage(1);
	}
}

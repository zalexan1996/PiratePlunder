using Godot;
using System;
using System.Diagnostics;

public partial class Explosion : Node2D
{
	[Export]
	public AnimatedSprite2D AnimatedSprite2D { get; set; }
	private void onAnimationFinished()
	{
		AnimatedSprite2D.Visible = false;
	}

	private void onExplosionEnd()
	{
		QueueFree();
	}
}

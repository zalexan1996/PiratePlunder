using Godot;
using System;

public partial class BloodExplosion : AnimatedSprite2D
{
	private void onAnimationEnd()
	{
		QueueFree();
	}
}

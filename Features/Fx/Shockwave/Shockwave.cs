using Godot;
using System;

public partial class Shockwave : Node2D
{
	[Export]
	public AnimatedSprite2D Explosion { get; set; }

	[Export]
	public AnimatedSprite2D Smoke { get; set; }

    public override void _Ready()
    {
		Explosion.AnimationFinished += onAnimationEnd;
		Smoke.AnimationFinished += onAnimationEnd;
    }
    private void onAnimationEnd()
	{
		if (!Explosion.IsPlaying() && !Explosion.IsPlaying())
		{
			QueueFree();
		}
	}

}

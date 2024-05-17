using Godot;
using System;

public interface IPlayerInputComponent
{
	public Vector2 InputVector { get; set; }
}
public partial class PlayerInputComponent : Node, IPlayerInputComponent
{
	[Signal]
	public delegate void FireEventHandler();
	public Vector2 InputVector { get; set; } = Vector2.Zero;

    public override void _Process(double delta)
    {
        base._Process(delta);
		InputVector = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if (@event.IsActionReleased("Fire"))
		{
			EmitSignal(SignalName.Fire);
		}
    }
}

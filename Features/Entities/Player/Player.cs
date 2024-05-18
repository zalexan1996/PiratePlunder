
using Godot;

public partial class Player : Node2D
{
    [Export]
    public PlayerInputComponent PlayerInputComponent { get; set; }
    private IPlayerInputComponent _PlayerInputComponent => PlayerInputComponent;

    [Export]
    public Ship Ship { get; set; }

    public override void _Ready()
    {
        PlayerInputComponent.Fire += onFire;
    }

    public override void _PhysicsProcess(double delta)
    {
        Ship.InputVector = _PlayerInputComponent.InputVector;
        Ship.AimLocation = GetGlobalMousePosition();

        base._PhysicsProcess(delta);
    }

    private void onFire()
    {
        Ship.Fire();
    }
}

using Godot;

public partial class Player : Node2D
{
    [Export]
    public PlayerInputComponent PlayerInputComponent { get; set; }
    private IPlayerInputComponent _PlayerInputComponent => PlayerInputComponent;

    [Export]
    public Ship Ship { get; set; }

    [Export]
    public PackedScene CannonballScene { get; set; }

    public override void _Ready()
    {
        PlayerInputComponent.Fire += onFire;
    }

    public override void _PhysicsProcess(double delta)
    {
        Ship.InputVector = _PlayerInputComponent.InputVector;

        base._PhysicsProcess(delta);
    }

    private void onFire()
    {
        var cannonball = CannonballScene.Instantiate<Cannonball>();
        cannonball.TargetLocation = GetGlobalMousePosition();
        cannonball.Owner = this;
        cannonball.StartingLocation = Ship.GlobalPosition;

        GetTree().CurrentScene.AddChild(cannonball);
    }
}
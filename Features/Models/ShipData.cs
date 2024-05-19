using Godot;

public partial class ShipData : Resource
{
    [Export]
    public float MaxSpeed { get; set; } = 250f;
    [Export]
    public float ReloadDuration { get; set; } = 1.5f;
    [Export]
    public int MaxHealth { get; set; } = 3;
}
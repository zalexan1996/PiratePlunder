using Godot;

public partial class ShipData : Resource
{
    [Export]
    public float MaxSpeed { get; set; } = 250f;

    [Export]
    public float ReloadDuration { get; set; } = 1.5f;
    
    [Export]
    public int MaxHealth { get; set; } = 3;

    [Export]
    public FactionResource FactionResource { get; set; }

    [Export]
    public ShipType ShipType { get; set; }

    [Export]
    public Texture2D BoatImage { get; set; }

    [Export]
    public Texture2D DamagedBoatImage { get; set; }
}
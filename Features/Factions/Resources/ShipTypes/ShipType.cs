using Godot;

public partial class ShipType : Resource
{
    [Export]
    public string ShipTypeId { get; set; }
    
    [Export]
    public string ShipTypeName { get; set; }

    [Export]
    public LootRatioResource LootRatios { get; set; }

    [Export]
    public PackedScene AIControllerScene { get; set; }

    [Export]
    public int BaseBountyWorth { get; set; } = 100;
}
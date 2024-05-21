using Godot;

public partial class ShipType : Resource
{
    [Export]
    public string ShipTypeId { get; set; }
    
    [Export]
    public string ShipTypeName { get; set; }

    [Export]
    public float MoneyLootFactor { get; set; }

    [Export]
    public float WoodLootFactor { get; set; }
    
    [Export]
    public float CannonballLootFactor { get; set; }

    [Export]
    public float FoodLootFactor { get; set; }
}
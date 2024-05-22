using Godot;

public partial class LootRatioResource : Resource
{
    [Export]
    public float Gold { get; set; }
    
    [Export]
    public float Wood { get; set; }
    
    [Export]
    public float Food { get; set; }
    
    [Export]
    public float Cannonball { get; set; }

    public float Total => Gold + Wood + Food + Cannonball;
}
using Godot;

public partial class FactionResource : Resource
{
    [Export]
    public string FactionId { get; set; }

    [Export]
    public string FactionName { get; set; }
} 
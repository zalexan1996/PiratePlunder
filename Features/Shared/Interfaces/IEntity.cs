using Godot;

public interface IEntity : ITakesDamage, IInFaction, IHasInventory
{
    public Vector2 GlobalPosition { get; set; }
}
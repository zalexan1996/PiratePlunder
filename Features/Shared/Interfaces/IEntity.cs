using Godot;

public interface IEntity : ITakesDamage, IInFaction
{
    public Vector2 GlobalPosition { get; set; }
}
using Godot;
using Godot.Collections;

public interface IInFaction
{
    public FactionResource GetFaction();
    public bool IsInFaction(FactionResource faction) => GetFaction().FactionId == faction.FactionId;    
    public bool IsHostileWith(IEntity entity);
}
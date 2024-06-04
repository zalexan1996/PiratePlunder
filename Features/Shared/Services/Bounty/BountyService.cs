using Godot;

public partial class BountyService : Node
{
    [Signal]
    public delegate void BountyUpdatedEventHandler(int newBounty);

    public int Bounty { get; private set; }

    public void Reset()
    {
        Bounty = 0;
    }
    
    public void ShipDestroyed(ShipData data, IEntity dealer)
    {
        if (dealer.GetFaction().FactionId == "X")
        {
            Bounty += data.ShipType.BaseBountyWorth;
            Bounty += (int)data.ShipType.LootRatios.Gold * 10;

            EmitSignal(SignalName.BountyUpdated, Bounty);
        }
    }

    public void SettlementLooted()
    {
        Bounty += 1000;
        EmitSignal(SignalName.BountyUpdated, Bounty);
    }

    public void GroundCannonDestroyed(IEntity dealer)
    {
        if (dealer.GetFaction().FactionId == "X")
        {
            
            Bounty += 50;
            EmitSignal(SignalName.BountyUpdated, Bounty);
        }
    }

    public int GetHighestBounty()
    {
        using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
        var line = saveGame.GetLine();
        return string.IsNullOrEmpty(line) ? 0 : line.ToInt();
    }

    public void SaveBounty()
    {
        EnsureCreated();

        using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.ReadWrite);
        var highestBounty = GetHighestBounty();


        if (Bounty > highestBounty)
        {
            saveGame.StoreLine(Bounty.ToString());
        }
        saveGame.Close();
    }

    public void EnsureCreated()
    {
        if (!FileAccess.FileExists("user://savegame.save"))
        {
            using var saveGame = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
            saveGame.Close();
        }
    }
}
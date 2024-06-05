using System.Diagnostics;
using Godot;

public partial class InventoryComponent : Node
{
	[Signal]
	public delegate void InventoryChangedEventHandler(int cannonballs, int food, int wood, int gold);

	[Export]
	public AudioStreamPlayer2D CoinPlayer { get; set; }

	[Export]
	public int Cannonballs { get; set; } = 0;
	
	[Export]
	public int Food { get; set; } = 0;
	
	[Export]
	public int Wood
	{
		get
		{
			return _wood;
		}
		set
		{
			_wood = value;
			EmitSignal(SignalName.InventoryChanged, Cannonballs, Food, Wood, Gold);
		}
	}
	private int _wood = 0;
	
	[Export]
	public int Gold { get; set; } = 0;


	public void PopulateFromData(LootRatioResource ratios)
	{
		Empty();

		var rng = new RandomNumberGenerator();
		var totalLoot = rng.RandiRange(10, 100) * ratios.LootMultiplier;

		Cannonballs = Mathf.FloorToInt(ratios.Cannonball / ratios.Total * totalLoot);
		Food = Mathf.FloorToInt(ratios.Food / ratios.Total * totalLoot);
		Wood = Mathf.FloorToInt(ratios.Wood / ratios.Total * totalLoot);
		Gold = Mathf.FloorToInt(ratios.Gold / ratios.Total * totalLoot);
	}

	public void TransferTo(InventoryComponent other)
	{
		other.Cannonballs += Cannonballs;
		other.Food += Food;
		other.Wood += Wood;
		other.Gold += Gold;

		Empty();
		other.TriggerChangedEvent();
	}

	private void TriggerChangedEvent()
	{
		EmitSignal(SignalName.InventoryChanged, Cannonballs, Food, Wood, Gold);
	}

	public void Empty()
	{
		Cannonballs = 0;
		Food = 0;
		Wood = 0;
		Gold = 0;
	}

	public void Print()
	{
		Debug.WriteLine($"Cannonballs: {Cannonballs}");
		Debug.WriteLine($"Food: {Food}");
		Debug.WriteLine($"Wood: {Wood}");
		Debug.WriteLine($"Gold: {Gold}");
	}

	public void PlaySound()
	{
		Debug.WriteLine("PlaySound");
		CoinPlayer.Play();
	}
}

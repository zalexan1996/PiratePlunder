using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class Player : CharacterBody2D, IEntity
{
    [Signal]
    public delegate void OnDeathEventHandler();

    [Export]
    public ShipData ShipData { get; set; }

	[Export]
	public RotationalAlignerComponent RotationalAlignerComponent { get; set; }
	private IRotationalAlignerComponent _RotationalAlignerComponent => RotationalAlignerComponent;

    [Export]
    public FactionComponent FactionComponent { get; set; }

	[Export]
	public Vector2 InputVector { get; set; } = Vector2.Zero;

	[Export]
	public HealthComponent HealthComponent { get; set; }

	[Export]
	public Node2D HealthDisplayPoint { get; set; }

    [Export]
    public PlayerInputComponent PlayerInputComponent { get; set; }
    private IPlayerInputComponent _PlayerInputComponent => PlayerInputComponent;

    [Export]
    public Ship Ship { get; set; }

    [Export]
    public Label RepairLabel { get; set; }

    [Export]
    public InventoryComponent InventoryComponent { get; set; }
    
	[Export]
	public VelocityComponent VelocityComponent { get; set; }
	private IVelocityComponent VelocityInterface => VelocityComponent;

    private ICollection<IInteractable> interactablesInRange = new Collection<IInteractable>();

    public override void _Ready()
    {
        VelocityComponent.MaxSpeed = ShipData.MaxSpeed;
        Ship.Cannon.ReloadDelay = ShipData.ReloadDuration;
        HealthComponent.MaxHealth = ShipData.MaxHealth;
        HealthComponent.CurrentHealth = ShipData.MaxHealth;
        HealthComponent.HealthChanged += onHealthChanged;
        //HealthComponent.ResetHealth();

        FactionComponent.Faction = ShipData.FactionResource;
        Ship.GetNode<Sprite2D>("Sprite").Texture = ShipData.BoatImage;
        PlayerInputComponent.Fire += onFire;
        RotationalAlignerComponent.RotationHandler = () => {
			return VelocityInterface.CurrentVelocity;
		};

		Ship.CannonAligner.RotationHandler = () => {
			return (Ship.AimLocation - GlobalPosition).Rotated(-Rotation);
		};

        
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("Repair") && !HealthComponent.IsMaxHealth())
        {
            if (InventoryComponent.Wood > 0)
            {
                var damage = HealthComponent.MaxHealth - HealthComponent.CurrentHealth;
                var healed = Mathf.Min(InventoryComponent.Wood / 10, damage);
                HealthComponent.Heal(healed);
                InventoryComponent.Wood -= (int)healed * 10;
            }
        }
        if (HealthComponent.CurrentHealth <= HealthComponent.MaxHealth / 2f && InventoryComponent.Wood >= 20)
        {
            RepairLabel.Visible = true;
        }
        else
        {
            RepairLabel.Visible = false;
        }
    }
    public override void _PhysicsProcess(double delta)
    {
		Vector2 direction = InputVector;

		if (!direction.IsZeroApprox())
		{
			VelocityInterface.ApplyInputVector(direction);
		}

		Velocity = VelocityInterface.CurrentVelocity;
		MoveAndSlide();

        InputVector = _PlayerInputComponent.InputVector;
        Ship.AimLocation = GetGlobalMousePosition();
        Ship.SetTrailIntensity(Mathf.Lerp(0, 1, Velocity.Length() / VelocityComponent.MaxSpeed));
        HealthDisplayPoint.Rotation = -Rotation;
        base._PhysicsProcess(delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionReleased("Interact") && interactablesInRange.Any())
        {
            var firstInteractable = interactablesInRange.First();

            if (firstInteractable.CanInteract())
            {
                firstInteractable.Interact(this);
            }
        }

        if (@event.IsActionReleased("Print"))
        {
            InventoryComponent.Print();
        }

    }

    private void onFire()
    {
        Ship.Fire();
    }
    public void TakeDamage(int damage, IEntity dealer)
    {
		HealthComponent.TakeDamage(1);
        if (HealthComponent.IsDead())
        {
            EmitSignal(SignalName.OnDeath);
            QueueFree();
        }

    }

    private void onInteractionAreaBodyEntered(Node2D node)
    {
        Debug.WriteLine("Enter");
        var interactable = node as IInteractable;

        if (interactable is not null && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
            interactable.ShowInteractText();
        }
    }

    private void onInteractionAreaBodyExited(Node2D node)
    {
        var interactable = node as IInteractable;

        if (interactable is not null && interactablesInRange.Contains(interactable))
        {
            interactable.HideInteractText();
            interactablesInRange.Remove(interactable);
        }
    }

    private void onHealthChanged(float currentHealth, float maxHealth)
    {
        
        if (currentHealth / (float)maxHealth <= 0.5 && ShipData.DamagedBoatImage is not null)
        {
            Ship.GetNode<Sprite2D>("Sprite").Texture = ShipData.DamagedBoatImage;
        }
        else
        {
            Ship.GetNode<Sprite2D>("Sprite").Texture = ShipData.BoatImage;
        }
    }

    public FactionResource GetFaction() => FactionComponent.Faction;

    public bool IsHostileWith(IEntity entity) => true;
}
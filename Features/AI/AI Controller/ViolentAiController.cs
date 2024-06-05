using System;
using System.Diagnostics;
using Godot;

public partial class ViolentAiController : AIController
{
    private PursueBehavior pursueBehavior;
    public GuardBehavior guardBehavior;

    public override void _Ready()
    {
        pursueBehavior = new PursueBehavior()
        {
            Controls = Controls,
            Target = Target
        };

        guardBehavior = new GuardBehavior()
        {
            Controls = Controls,
        };

        Controls.ProximityDetectionComponent.EntityDetected += startFollow;
        Controls.ProximityDetectionComponent.EntityLeft += stopFollow;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (pursueBehavior.Target is null)
        {
            guardBehavior.Execute();
        }
        else
        {
            pursueBehavior.Execute();
        }
    }

    private void startFollow(IEntity entity)
    {
        pursueBehavior.StartFollow(entity);
    }
    private void stopFollow(IEntity entity)
    {
        pursueBehavior.StopFollow(entity);
    }
}
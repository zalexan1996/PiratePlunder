using System;
using System.Diagnostics;
using Godot;

public partial class ViolentAiController : AIController
{
    private PursueBehavior behavior;

    public override void _Ready()
    {
        behavior = new PursueBehavior()
        {
            Controls = Controls,
            Target = Target
        };

        Controls.ProximityDetectionComponent.EntityDetected += behavior.StartFollow;
        Controls.ProximityDetectionComponent.EntityLeft += behavior.StopFollow;
    }

    public override void _PhysicsProcess(double delta)
    {
        behavior.Execute();
    }
}
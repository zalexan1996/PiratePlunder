using Godot;
using System;

public partial class HitboxComponent : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
}

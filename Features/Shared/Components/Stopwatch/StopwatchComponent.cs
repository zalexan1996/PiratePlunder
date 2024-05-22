using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public partial class StopwatchComponent : Timer
{
	public int ElapsedSeconds { get; private set; } = 0;
	public float ElapsedMinutes => ElapsedSeconds / 60f;
	
	private SortedList<int, Action> behaviors = new();

	public override void _Ready()
	{
		Timeout += onTimeout;
	}

	public void Tap(int seconds, Action behavior)
	{
		behaviors.Add(seconds, behavior);
	}

	private void onTimeout()
	{
		ElapsedSeconds++;
		
		while (behaviors.Any() && behaviors.First().Key <= ElapsedSeconds)
		{
			var el = behaviors.First();
			el.Value();
			behaviors.Remove(el.Key);
		}
	}

}

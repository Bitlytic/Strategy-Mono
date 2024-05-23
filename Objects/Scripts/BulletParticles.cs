using Godot;
using System;

public partial class BulletParticles : GpuParticles2D
{

	private Node2D _target;


	public override void _Ready()
	{
		// Here, we reparent the node to the root but store the parent as our follow target
		// We do this because if the parent gets freed, all of the particles disappear immediately.
		_target = GetParent<Node2D>();

		Finished += OnFinished;
		Reparent(GetTree().Root);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_target != null && IsInstanceValid(_target))
		{
			GlobalPosition = _target.GlobalPosition;
		}
		else
		{
			Emitting = false;
		}
	}

	// Finished is called when all particles have disappeared
	private void OnFinished()
	{
		QueueFree();
	}
}

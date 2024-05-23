using Godot;
using System;

public abstract partial class EnemyState : Node
{

	/* 
	* This is the base enemy state
	* Each state will inherit from this
	*/

	[Signal]
	public delegate void TransitionedEventHandler(EnemyState state, string newStateName);

	protected Enemy _enemy;
	protected Player _player;


	public override void _Ready()
	{
		_player = (Player)GetTree().GetFirstNodeInGroup("player");
		_enemy = GetOwner<Enemy>();
		_enemy.Damaged += OnDamaged;
	}

	
	// This is called directly when transitioning to this state
	// Useful for setting up the state to be used
	// In Idle, we use this function to decide how long we will idle for
	public virtual void Enter() { }


	// When the state is active, this is essentially the _Process() function
	public virtual void ProcessState(double delta) { }

	// When the state is active, this is essentially the _PhysicsProcess() function
	public virtual void PhysicsProcessState(double delta) { }


	// Useful for cleaning up the state
	// For example, clearing any timers, disconnecting any signals, etc.
	public virtual void Exit() { }


	/*
	* Non FSM-specific methods. These are utility 
	* methods that may be used by multiple states.
	*/


	// Attempts to swtich to chase state if it detects the player
	protected bool TryChase()
	{
		if (GetDistanceToPlayer() <= _enemy.DetectionRadius)
		{
			EmitSignal(SignalName.Transitioned, this, "chase");
			return true;
		}

		return false;
	}

	protected float GetDistanceToPlayer()
	{
		return _player.GlobalPosition.DistanceTo(_enemy.GlobalPosition);
	}

	// If you wanted to replace this functionality in a state you can either:
	// 1. Disconnect the signal by doing _enemy.Damaged -= OnDamaged
	// 2. Override the OnDamaged() function to do nothing
	// 3. Override the _Ready() function
	// This is the order I would recommend personally
	private void OnDamaged(Attack attack)
	{
		EmitSignal(SignalName.Transitioned, this, "stun");
	}
}

using Godot;
using System;

public partial class EnemyIdle : EnemyState
{
    private Timer _idleTimer;


    // Upon moving to this state, intialize the
    // timer with a random duration.
    public override void Enter()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();

        _enemy.Velocity = Vector2.Zero;

        _idleTimer = new Timer();
        _idleTimer.WaitTime = random.RandfRange(3, 10);
        _idleTimer.Timeout += OnTimeout;
        _idleTimer.Autostart = true;
        AddChild(_idleTimer);
    }

    private void OnTimeout()
    {
        EmitSignal(SignalName.Transitioned, this, "wander");
    }

    public override void PhysicsProcessState(double delta)
    {
        TryChase();
    }

    
    // When leaving this state (for any reason), stop timer,
    // disconnect signals, and free timer
    // Technically, just QueueFree() would be required, but
    // I like showcasing all of the options
    public override void Exit()
    {
        _idleTimer.Stop();
        _idleTimer.Timeout -= OnTimeout;
        _idleTimer.QueueFree();
        _idleTimer = null;
    }

}

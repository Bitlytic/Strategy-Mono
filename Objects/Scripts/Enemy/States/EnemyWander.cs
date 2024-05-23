using Godot;
using System;

public partial class EnemyWander : EnemyState
{

    [Export]
    private float _minWanderTime = 2.5f;

    [Export]
    private float _maxWanderTime = 10.0f;

    [Export]
    private float _wanderSpeed = 50.0f;

    private Vector2 _wanderDirection;
    private Timer _wanderTimer;


    // Upon moving to this state, initialize the
    // timer with a random duration.
    public override void Enter()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();

        float randomRotation = random.RandfRange(0, 360);
        _wanderDirection = Vector2.Up.Rotated(Mathf.DegToRad(randomRotation));

        _wanderTimer = new Timer();
        _wanderTimer.WaitTime = random.RandfRange(_minWanderTime, _maxWanderTime);
        _wanderTimer.Timeout += OnTimeout;
        _wanderTimer.Autostart = true;
        AddChild(_wanderTimer);
    }


    private void OnTimeout() {
        EmitSignal(SignalName.Transitioned, this, "Idle");
    }

    public override void PhysicsProcessState(double delta)
    {
        _enemy.Velocity = _wanderDirection * _wanderSpeed;
        _enemy.MoveAndSlide();

        TryChase();
    }



    // When leaving this state (for any reason), stop timer,
    // disconnect signals, and free timer
    // Technically, just queue_free() would be required, but
    // I like showcasing all of the options
    public override void Exit() {
        _wanderTimer.Stop();
        _wanderTimer.Timeout -= OnTimeout;
        _wanderTimer.QueueFree();
        _wanderTimer = null;
    }

}

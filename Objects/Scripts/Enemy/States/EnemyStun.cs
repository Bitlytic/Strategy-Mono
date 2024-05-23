using Godot;
using System;

public partial class EnemyStun : EnemyState
{
    private Timer _timer;


    // Upon moving to this state, intialize the
    // timer with a random duration.
    public override void Enter()
    {
        Random random = new Random();

        _timer = new Timer();
        _timer.WaitTime = 1.0;
        _timer.Timeout += OnTimeout;
        _timer.Autostart = true;
        AddChild(_timer);

        _enemy.Stunned = true;
    }

    private void OnTimeout()
    {
        EmitSignal(SignalName.Transitioned, this, "chase");
    }

    // Upon leaving this state, clear and free all
    // state relevant stuff
    public override void Exit()
    {
        _timer.Stop();
        _timer.Timeout -= OnTimeout;
        _timer.QueueFree();
        _timer = null;
        _enemy.Stunned = false;
    }

}

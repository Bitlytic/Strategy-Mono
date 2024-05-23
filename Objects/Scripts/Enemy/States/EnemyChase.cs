using Godot;
using System;

public partial class EnemyChase : EnemyState
{
    [Export]
    private float _chaseSpeed = 75.0f;


    public override void PhysicsProcessState(double delta)
    {
        Vector2 direction = _player.GlobalPosition - _enemy.GlobalPosition;

        float distance = direction.Length();
        if (distance > _enemy.ChaseRadius)
        {
            EmitSignal(SignalName.Transitioned, this, "wander");
            return;
        }

        _enemy.Velocity = direction.Normalized() * _chaseSpeed;

        if (distance <= _enemy.FollowRadius)
        {
            _enemy.Velocity = Vector2.Zero;
        }

        _enemy.MoveAndSlide();
    }
}

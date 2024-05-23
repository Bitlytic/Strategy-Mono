using Godot;
using System;

public partial class PlayerCamera : Camera2D
{

    [Export]
    private Player _target;

    [Export]
    private float _sensitivity = 0.1f;


    public override void _PhysicsProcess(double delta)
    {
        Vector2 targetPosition = _target.AimPosition * _sensitivity;

        Position = Position.Lerp(targetPosition, 0.25f);
    }
}

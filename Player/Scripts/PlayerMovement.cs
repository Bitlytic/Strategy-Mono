using Godot;
using System;

public partial class PlayerMovement : Node
{

    /*
    * This is the player's movement controller.
    * Instead of placing all of the movement stuff inside
    * of the player, we move the code to a separate component
    */

    [ExportGroup("Speed Values")]
    [Export]
    private float _maxSpeed = 100;

    [Export]
    private float _accelerationTime = 0.1f;

    private Player _player;


    public override void _Ready()
    {
        _player = GetOwner<Player>();
    }


    public override void _PhysicsProcess(double delta)
    {
        // Read the _player's current velocity
        Vector2 velocity = _player.Velocity;

        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");


        // Apply any changes to velocity
        velocity = velocity.MoveToward(inputDirection*_maxSpeed, (1.0f / _accelerationTime) * (float)delta * _maxSpeed);

        if (inputDirection.Y != 0 && Mathf.Sign(inputDirection.Y) != Mathf.Sign(velocity.Y))
        {
            velocity.Y *= 0.75f;
        }

        if (inputDirection.X != 0 && Mathf.Sign(inputDirection.X) != Mathf.Sign(velocity.X))
        {
            velocity.X *= 0.75f;
        }

        // Reassign velocity and move the player
        _player.Velocity = velocity;
        _player.MoveAndSlide();
    }



}

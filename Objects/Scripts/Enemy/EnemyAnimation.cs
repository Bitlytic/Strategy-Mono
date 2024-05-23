using Godot;
using System;

public partial class EnemyAnimation : Node
{
    /*
    * This controls all animations for the enemy.
    * You totally could use an AnimationTree for this,
    * but this is an example of how you could control
    * it through code. 
    */

    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private Sprite2D _sprite;

    private Enemy _enemy;


    public override void _Ready()
    {
        _enemy = GetOwner<Enemy>();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_enemy.Alive)
        {
            return;
        }

        if (_enemy.Stunned)
        { 
            _animationPlayer.Play("stunned");
            return;
        }

        if (_enemy.Velocity == Vector2.Zero)
        {
            _animationPlayer.Play("idle");
            return;
        }

        _sprite.FlipH = _enemy.Velocity.X < 0;

        string animationName = "walk";

        if (_enemy.Velocity.Length() > 50)
        {
            animationName = "run";
        }

        if (_sprite.FlipH)
        {
            animationName += "_left";
        }
        else
        {
            animationName += "_right";
        }

        _animationPlayer.Play(animationName);
    }

}

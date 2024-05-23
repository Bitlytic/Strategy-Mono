using Godot;
using System;

public partial class PlayerAnimation : Node
{
    /*
    * Similar to the movement component. The actual 
    * animations are handled through an AnimationTree 
    * for the player, but parameters are updated here.
    */

    [Export]
    private AnimationTree _animationTree;

    [Export]
    private Sprite2D _sprite;

    private Player _player;

    public override void _Ready()
    {
        //The animation tree is inactive while outside of gameplay.
        //This makes it easier to edit animations in the editor.
        _animationTree.Active = true;
        _player = GetOwner<Player>();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = _player.Velocity;

        if (velocity != Vector2.Zero)
        {
            float timeScale = 1;

            if (Mathf.Sign(_player.AimPosition.X) != Mathf.Sign(velocity.X)) {
                timeScale = -1;
            }

            _animationTree.Set("parameters/TimeScale/scale", timeScale);
            _animationTree.Set("parameters/WalkDirection/blend_position", Mathf.Sign(_player.AimPosition.X));
        }
        else
        {
            _sprite.FlipH = _player.AimPosition.X < 0;
            _animationTree.Set("parameters/TimeScale/scale", 1);
            _animationTree.Set("parameters/WalkDirection/blend_position", 0);
        }

    }
}

using Godot;
using System;

public partial class PlayerWeapon : Node2D
{

    [Export]
    private Marker2D _firingPosition;

    private Player _player;
    private PackedScene _bulletScene;


    public override void _Ready()
    {
        _bulletScene = GD.Load<PackedScene>("res://Objects/Scenes/bullet.tscn");
        _player = GetOwner<Player>();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("primary_fire"))
        {
            // If we're aiming at a different side, flip the firing position across the X axis
            if (Mathf.Sign(_player.AimPosition.X) != Mathf.Sign(_firingPosition.Position.X))
            {
                Vector2 newPos = _firingPosition.Position;
                newPos.X *= -1;
                _firingPosition.Position = newPos;
            }

            // Spawn a bullet and give it a rotation based on the angle between the firing position and
            // the cursor's position.
            // The bullet will use this rotation to decide its direction.
            Bullet spawnedBullet = _bulletScene.Instantiate<Bullet>();
            Vector2 mouseDirection = GetGlobalMousePosition() - _firingPosition.GlobalPosition;

            GetTree().Root.AddChild(spawnedBullet);
            spawnedBullet.GlobalPosition = _firingPosition.GlobalPosition;
            spawnedBullet.Rotation = mouseDirection.Angle();

            /*
            * Strategy Relevant Code:
            * Here, we loop over all of the upgrades currently
            * on the player, and apply their upgrade to the
            * spawned bullet.
            */

            foreach (var strategy in _player.Upgrades)
            {
                if (strategy is IBulletStrategy bulletStrategy)
                {
                    bulletStrategy.ApplyUpgrade(spawnedBullet);
                }
            }
        }
    }

}

using Godot;
using System;

[GlobalClass]
public partial class SpeedBulletStrategy : BaseBulletStrategy, IBulletStrategy
{
    [Export]
    public float _speed = 50.0f;

    public void ApplyUpgrade(Bullet bullet)
    {
        bullet.Speed += _speed;
    }
}

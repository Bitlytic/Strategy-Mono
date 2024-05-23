using Godot;
using System;

[GlobalClass]
public partial class DamageBulletStrategy : BaseBulletStrategy, IBulletStrategy
{
    [Export]
    private float _damage = 5.0f;

    public void ApplyUpgrade(Bullet bullet)
    {
        bullet.Damage += _damage;
    }
}

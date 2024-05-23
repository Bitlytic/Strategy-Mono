using Godot;
using System;

[GlobalClass]
public partial class PierceBulletStrategy : BaseBulletStrategy, IBulletStrategy
{
    public void ApplyUpgrade(Bullet bullet)
    {
        bullet.MaxPierce++;
    }
}

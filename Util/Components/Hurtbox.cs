using Godot;
using System;

public partial class Hurtbox : Area2D
{

    [Signal]
    public delegate void HitEnemyEventHandler();

    private Bullet _bullet;


    public override void _Ready()
    {
        _bullet = GetOwner<Bullet>();
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Hitbox hitbox)
        {
            Attack attack = new Attack(_bullet.Damage);
            hitbox.Damage(attack);

            EmitSignal(SignalName.HitEnemy);
        }

    }
}

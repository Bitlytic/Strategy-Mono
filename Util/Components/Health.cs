using Godot;
using System;

[GlobalClass]
public partial class Health : Node
{

    /*
    * Health Component for enemies
    */

    [Signal]
    public delegate void HealthChangedEventHandler(float health);

    [Export]
    private Hitbox _hitbox;

    [Export]
    private AnimationPlayer _animationPlayer;

    [Export]
    private float _maxHealth = 10.0f;
    private float _health;

    private Enemy _enemy;

    public override void _Ready()
    {
        _health = _maxHealth;
        _enemy = GetOwner<Enemy>();

        if (_hitbox != null)
        {
            _hitbox.Damaged += OnDamaged;
        }
    }


    private void OnDamaged(Attack attack)
    {
        if (!_enemy.Alive)
        {
            return;
        }

        _health -= attack.Damage;
        EmitSignal(SignalName.HealthChanged, _health);

        if (_health <= 0 )
        {
            _health = 0;
            _enemy.Alive = false;
            if (_animationPlayer != null)
            {
                _animationPlayer.Play("death");
            }
        }
    }


}

using Godot;
using System;

public partial class Bullet : CharacterBody2D
{

	[Export]
	private Hurtbox _hurtbox;

	[Export]
	public float Speed { get; set; } = 150.0f;

	[Export]
	public float Damage { get; set; } = 5.0f;

	[Export]
	public int MaxPierce { get; set; } = 1;

	private int _currentPierceCount = 0;


	public override void _Ready()
	{
		if (_hurtbox != null)
		{
			_hurtbox.HitEnemy += OnEnemyHit;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Right.Rotated(Rotation);

		Velocity = direction * Speed;

		KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);

		if (collision != null)
		{
			QueueFree();
		}
	}

	private void OnEnemyHit()
	{
		_currentPierceCount++;

		if (_currentPierceCount >= MaxPierce)
		{
			QueueFree();
		}
	}

}

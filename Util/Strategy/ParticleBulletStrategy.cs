using Godot;
using System;

[GlobalClass]
public partial class ParticleBulletStrategy : BaseBulletStrategy, IBulletStrategy
{
	public void ApplyUpgrade(Bullet bullet)
	{
		PackedScene particleScene = GD.Load<PackedScene>("res://Objects/Scenes/bullet_particles.tscn");

		Node2D spawnedParticles = particleScene.Instantiate<Node2D>();
		bullet.AddChild(spawnedParticles);
		spawnedParticles.GlobalPosition = bullet.GlobalPosition;
	}

}

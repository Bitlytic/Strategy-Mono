using Godot;
using Godot.Collections;
using System;

public partial class Enemy : CharacterBody2D
{

	/*
	* I tend to keep the top level node's functionality 
	* small. Here, this node is responsible for common state
	* variables, passing the damaged signal around, and 
	* picking a random texture on spawn.
	*
	* For the most part, other functionality that controls
	* the enemy is handled by specific states.
	*
	* ex. Movement is handled by states setting velocity
	* and calling MoveAndSlide()
	*/


	[Signal]
	public delegate void DamagedEventHandler(Attack attack);

	public bool Alive { get; set; } = true;
	public bool Stunned { get; set; } = false;

	[ExportGroup("Textures")]
	
	[Export]
	private Texture2D[] _textures  = new Texture2D[0];


	[ExportGroup("Vision Ranges")]

	[Export]
	public float DetectionRadius { get; set; } = 100;

	[Export]
	public float ChaseRadius { get; set; } = 200;

	[Export]
	// This guy doesn't actually attack, he just tries to get close to the player
	public float FollowRadius { get; set; } = 25;


	private Sprite2D _sprite;


	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");

		Random random = new Random();
		_sprite.Texture = _textures[random.Next(_textures.Length)];
	}

	private void OnDamaged(Attack attack)
	{
		EmitSignal(SignalName.Damaged, attack);
	}

}

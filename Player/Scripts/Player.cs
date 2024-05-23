using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{

    /*
	* # Strategy Relevant Code:
	* This is where the upgrades are stored
	*/
	public List<BaseBulletStrategy> Upgrades { get; set; } = new List<BaseBulletStrategy>();

	public Vector2 AimPosition { get; set; } = new Vector2(1, 0);

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			Vector2 halfViewport = GetViewportRect().Size / 2.0f;
			AimPosition = (mouseMotion.Position - halfViewport);
		}
	}
}

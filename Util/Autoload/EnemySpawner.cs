using Godot;
using System;

public partial class EnemySpawner : Node2D
{
    /*
    * This is an autoloaded node that allows you
    * to spawn an enemy when pressing "E" by default
    */

    private PackedScene _enemyScene;


    public override void _Ready()
    {
        _enemyScene = GD.Load<PackedScene>("res://Objects/Scenes/enemy.tscn");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("spawn_enemy"))
        {
            Enemy spawnedEnemy = _enemyScene.Instantiate<Enemy>();
            GetTree().Root.AddChild(spawnedEnemy);
            spawnedEnemy.GlobalPosition = GetGlobalMousePosition();
        }
    }
}

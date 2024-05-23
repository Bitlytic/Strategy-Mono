using Godot;
using System;

/*
* Tools are sick. The code in this script is treated
* nearly the same as if the game is running. The one
* difference is we can do Engine.is_editor_hint() to
* find out if we're currently in the editor
* Docs: https://docs.godotengine.org/en/stable/tutorials/plugins/running_code_in_the_editor.html#how-to-use-tool
*/

public partial class Upgrade : Area2D
{
	[Export]
	private Label _upgradeLabel;

	[Export]
	private Sprite2D _sprite;

	[Export]
	private BaseBulletStrategy _strategy = new BaseBulletStrategy();


	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		_sprite.Texture = _strategy.Texture;
		_upgradeLabel.Text = _strategy.UpgradeText;
	}

	public override void _Process(double delta)
	{
		// This is run only when we're editing the scene
		// It updates the texture of the sprite when we replace the upgrade strategy
		// NOTE: For some reason, this breaks in C# for me. You may have more luck comparing it
		// to the GDScript version and attempting it yourself. There seems to be a bug where
		// resources and tool scripts in C# don't play well.
		if (Engine.IsEditorHint())
		{
			if (_strategy != null)
			{
				_sprite.Texture = _strategy.Texture;
				_upgradeLabel.Text = _strategy.UpgradeText;
			}
		}
	}


	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.Upgrades.Add(_strategy);
			QueueFree();
		}
	}
}

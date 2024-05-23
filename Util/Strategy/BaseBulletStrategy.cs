using Godot;
using System;

[GlobalClass]
public partial class BaseBulletStrategy : Resource
{
    [Export]
    public Texture2D Texture { get; set; }


    [Export]
    public string UpgradeText { get; set; } = "Speed";
}

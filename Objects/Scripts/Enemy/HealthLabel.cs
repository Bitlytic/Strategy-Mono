using Godot;
using System;

public partial class HealthLabel : Label
{

    public void OnHealthChanged(float newHealth)
    {
        Text = "Health: " + newHealth;
    }
}

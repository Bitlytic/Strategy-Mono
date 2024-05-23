using Godot;
using System;

[GlobalClass]
public partial class Hitbox : Area2D
{
    /*
    * Hitbox component - this gets hit by the bullet's hurtbox.
    * Only used by enemies by default, but can be referenced by
    * health components to receive damage
    */

    [Signal]
    public delegate void DamagedEventHandler(Attack attack);

    public void Damage(Attack attack)
    {
        EmitSignal(SignalName.Damaged, attack);
    }

}

using Godot;
using Godot.Collections;
using System;

public partial class EnemyStateMachine : Node
{
    /*
    * State machine as set up in my video on FSMs
    * https://www.youtube.com/watch?v=ow_Lum-Agbs
    * Code is clearer here though, better names
    */

    [Export]
    private EnemyState _initialState;

    private EnemyState _currentState;
    private Dictionary<string, EnemyState> _states = new Dictionary<string, EnemyState>();

    public override void _Ready()
    {
        foreach (Node child in GetChildren())
        {
            if (child is EnemyState state)
            {
                _states.Add(state.Name.ToString().ToLower(), state);
                state.Transitioned += OnChildTransition;
            }
        }

        if (_initialState != null)
        {
            _currentState = _initialState;
            _initialState.Enter();
        }
    }

    public override void _Process(double delta)
    {
        if (_currentState != null)
        {
            _currentState.ProcessState(delta);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_currentState != null)
        {
            _currentState.PhysicsProcessState(delta);
        }
    }

    public void OnChildTransition(EnemyState state, string newStateName)
    {
        if (state != _currentState)
        {
            return;
        }

        EnemyState newState = _states[newStateName.ToLower()];
        if (newState == null)
        {
            return;
        }

        // Clean up the previous state
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        // Initialize the new state
        newState.Enter();
        _currentState = newState;

    }
}

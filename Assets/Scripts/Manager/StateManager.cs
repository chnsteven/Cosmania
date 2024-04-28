using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Playables;

public class StateManager
{
    public IState initialState;
    public IState currentState;
    [HideInInspector] public string nameOfChangedDialogVariable;
    [HideInInspector] public Ink.Runtime.Object valueOfChangedDialogVariable;
    [HideInInspector] public bool dialogEnded = true;

    public StateManager(IState _initialState)
    {
        initialState = _initialState;
    }

    public void Start()
    {
        currentState = initialState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentState == null) return;
        currentState.UpdateState();
    }
    public void SwitchState(IState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState(this);
    }
}


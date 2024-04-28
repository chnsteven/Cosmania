using System;
using UnityEngine;

public interface IState
{
    public void EnterState(StateManager stateManager);
    public void UpdateState();
    public void ExitState();
}


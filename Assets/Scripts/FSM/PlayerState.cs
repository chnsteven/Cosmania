using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerInvestigationState : IState
{
    public static event Action<bool> ShowInvestigationUI;
    public static event Action Countdown;
    public static event Action PlayerInInvestigationState;
    public static event Action PlayerEnterInvestigationState;
    public void EnterState(StateManager _stateManager)
    {
        Gamepad.instance.isDisabled = true;
        PlayerEnterInvestigationState?.Invoke();
        ShowInvestigationUI?.Invoke(true);
        Countdown?.Invoke();
    }

    public void ExitState()
    {
        ShowInvestigationUI?.Invoke(false);
        //Cursor.lockState = CursorLockMode.None;
        DialogManager.instance.EnterDialogMode();
    }

    public void UpdateState()
    {
        PlayerInInvestigationState?.Invoke();
    }
}

public class PlayerIdleState : IState
{
    public static event Action PlayerInIdleState;
    public void EnterState(StateManager _stateManager)
    {
        //Debug.Log("Player Enter Idle State");
        Cursor.lockState = CursorLockMode.None;

    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
        PlayerInIdleState?.Invoke();
    }
}
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Playables;

public class NPCGuideState : IState
{
    public static event Action<bool> NPCInGuideState;
    private StateManager stateManager;
    public void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        //Debug.Log("Enter Guide State");
        NPCInGuideState?.Invoke(true);
        NPCMovement.instance.FollowPath();
    }

    public void ExitState()
    {
        NPCInGuideState?.Invoke(false);
    }

    public void UpdateState()
    {
        if (NPCMovement.instance.isFinished)
        {
            stateManager.SwitchState(NPCStateManager.instance.IdleState);
            return;
        }
    }
}

public class NPCTalkState : IState
{
    public static event Action<bool> NPCInTalkState;
    private StateManager stateManager;
    public void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        stateManager.dialogEnded = false;
        //Debug.Log("Enter Talk State");
        NPCInTalkState?.Invoke(true);
        DialogManager.instance.EnterDialogMode();
    }

    public void ExitState()
    {
        //Debug.Log("Exit Talk State");
        NPCInTalkState?.Invoke(false);
        stateManager.dialogEnded = true;

    }

    public void UpdateState()
    {
        if (NPCStateManager.instance.dialogEnded)
        {
            if (NPCStateManager.instance.nameOfChangedDialogVariable == "currentState" &&
                NPCStateManager.instance.valueOfChangedDialogVariable.ToString() == "guiding")
            {
                stateManager.SwitchState(NPCStateManager.instance.GuideState);
                return;
            }
            if (NPCStateManager.instance.nameOfChangedDialogVariable == "currentState" &&
                NPCStateManager.instance.valueOfChangedDialogVariable.ToString() == "nextScene")
            {
                stateManager.SwitchState(NPCStateManager.instance.TimelineState);
                return;
            }
            stateManager.SwitchState(NPCStateManager.instance.IdleState);
            return;
        }


    }
}
public class NPCIdleState : IState
{
    public static event Action<bool> NPCInIdleState;
    private StateManager stateManager;
    public void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        //Debug.Log("Enter Idle State");
        NPCInIdleState?.Invoke(stateManager.dialogEnded);
        //animator = stateManager.GetComponent<Animator>();
        //if (animator == null) return;
    }

    public void ExitState()
    {
        NPCInIdleState?.Invoke(stateManager.dialogEnded);
    }

    public void UpdateState()
    {

    }
}

public class NPCTimelineState : IState
{
    public static event Action<bool> NPCInTimelineState;
    private StateManager stateManager;
    private PlayableDirector playable;
    public void EnterState(StateManager _stateManager)
    {
        stateManager = _stateManager;
        playable = NPCStateManager.instance.playable;
        //Debug.Log("Enter Timeline State");
        NPCInTimelineState?.Invoke(true);
        if (playable != null)
            //Debug.Log("Play timeline");
            playable.Play();
    }

    public void ExitState()
    {
        NPCInTimelineState?.Invoke(false);
    }

    public void UpdateState()
    {
    }
}

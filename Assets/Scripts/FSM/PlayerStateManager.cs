using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager instance;
    public StateManager stateManager;
    public IState InvestigationState = new PlayerInvestigationState();
    public IState IdleState = new PlayerIdleState();
    [HideInInspector] public string nameOfChangedDialogVariable;
    [HideInInspector] public Ink.Runtime.Object valueOfChangedDialogVariable;


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Debug.LogError("More than 1 player state manager");
        instance = this;
        stateManager = new StateManager(IdleState);
        stateManager.Start();
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.Update();
    }
    public void SwitchState(IState state)
    {
        stateManager.SwitchState(state);
    }

    private void OnEnable()
    {
        DialogVariables.NotifyVariableChanged += DialogVariableChanged;
    }

    private void OnDisable()
    {
        DialogVariables.NotifyVariableChanged -= DialogVariableChanged;
    }

    private void DialogVariableChanged(string name, Ink.Runtime.Object value)
    {
        nameOfChangedDialogVariable = name;
        valueOfChangedDialogVariable = value;
    }
}

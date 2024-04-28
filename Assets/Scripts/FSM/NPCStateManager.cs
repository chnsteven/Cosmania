using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCStateManager : MonoBehaviour
{
    public static NPCStateManager instance;
    public StateManager stateManager;
    public IState GuideState = new NPCGuideState();
    public IState TalkState = new NPCTalkState();
    public IState IdleState = new NPCIdleState();
    public IState TimelineState = new NPCTimelineState();
    [HideInInspector] public string nameOfChangedDialogVariable;
    [HideInInspector] public Ink.Runtime.Object valueOfChangedDialogVariable;
    [HideInInspector] public bool dialogEnded = true;
    public PlayableDirector playable;

    // Start is called before the first frame update
    void Start()
    {
        playable = GetComponent<PlayableDirector>();
        if (instance != null) Debug.LogError("More than 1 npc state manager");
        instance = this;
        stateManager = new StateManager(IdleState);
        stateManager.Start();
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.Update();
    }
    public void SwitchState(IState state) {
        stateManager.SwitchState(state);
    }

    private void OnEnable()
    {
        DialogVariables.NotifyVariableChanged += DialogVariableChanged;
        DialogManager.EndOfStory += DialogEnded;
        playable.stopped += DestroyNPC;
    }

    private void OnDisable()
    {
        DialogVariables.NotifyVariableChanged -= DialogVariableChanged;
        DialogManager.EndOfStory -= DialogEnded;
        playable.stopped -= DestroyNPC;
    }

    private void DestroyNPC(PlayableDirector playable)
    {
        Destroy(this.gameObject);
    }

    private void DialogVariableChanged(string name, Ink.Runtime.Object value) {
        nameOfChangedDialogVariable = name;
        valueOfChangedDialogVariable = value;
    }

    private void DialogEnded() {
        dialogEnded = true;
    }
}

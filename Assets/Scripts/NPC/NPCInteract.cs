using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour, IInteractable
{
    public IInteractableStruct s;
    public static NPCInteract instance;

    void Start()
    {
        if (instance != null) Debug.LogError("only 1 instance of NPC interact");
        instance = this;
        s = new IInteractableStruct("Talk to NPC", true);
        
    }
    public string GetInteractText()
    {
        return s.interactText;
    }

    public void Interact(Transform interactorTransform)
    {
        if (s.isInteractable) NPCStateManager.instance.stateManager.SwitchState(NPCStateManager.instance.TalkState);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsInteractable()
    {
        return s.isInteractable;
    }

    public void GetNotified(string name, Ink.Runtime.Object value)
    {
    }

    private void InGuideState(bool inGuideState) {
        s.isInteractable = !inGuideState;

    }

    private void InTalkState(bool inTalkState)
    {
        s.isInteractable = !inTalkState;
    }

    private void OnEnable()
    {
        DialogVariables.NotifyVariableChanged += GetNotified;
        NPCGuideState.NPCInGuideState += InGuideState;
        NPCTalkState.NPCInTalkState += InTalkState;
    }

    private void OnDisable()
    {
        DialogVariables.NotifyVariableChanged -= GetNotified;
        NPCGuideState.NPCInGuideState -= InGuideState;
        NPCTalkState.NPCInTalkState -= InTalkState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLampInteract : MonoBehaviour, IInteractable
{
    private IInteractableStruct s;
    private Animator animator;
    private bool isOn = true;

    void Start()
    {
        s = new IInteractableStruct("Turn On/Off", true);
        animator = GetComponent<Animator>();
    }
    public string GetInteractText()
    {
        return s.interactText;
    }

    public bool GetIsInteractable()
    {
        return s.isInteractable;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        animator.SetBool("TurnOn", !isOn);
        isOn = !isOn;
    }

    public void GetNotified(string name, Ink.Runtime.Object value)
    {
    }

    private void OnEnable()
    {
        DialogVariables.NotifyVariableChanged += GetNotified;

    }

    private void OnDisable()
    {
        DialogVariables.NotifyVariableChanged -= GetNotified;

    }
}

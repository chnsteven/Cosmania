using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerInteract : MonoBehaviour, IInteractable
{
    private IInteractableStruct s;
    [SerializeField]
    private Animator animator;
    [SerializeField] private GameObject trap;
    void Start()
    {
        s = new IInteractableStruct("Press the button",
            true);
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
        if (trap == null) return;
        if (s.isInteractable)
        {
            animator.SetTrigger("Press");
            trap.SetActive(true);
            s.isInteractable = !s.isInteractable;
        }
    }

    // Update is called once per frame
    void Update()
    {

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

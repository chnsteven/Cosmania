using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactorTransform);
    string GetInteractText();
    Transform GetTransform();
    bool GetIsInteractable();
    void GetNotified(string name, Ink.Runtime.Object value);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IInteractableStruct
{
    public string interactText;
    public bool isInteractable;

    public IInteractableStruct(string _interactText, bool _isInteractable)
    {
        interactText = _interactText;
        isInteractable = _isInteractable;
    }
}

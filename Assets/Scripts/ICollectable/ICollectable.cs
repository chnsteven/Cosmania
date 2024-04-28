using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    void AddToInventory(Collider collider, int cost);
    string GetDescriptionText();
    void SetIsInteractable(Collider collider, int cost);
    int GetTimeCost();
    string GetName();
}

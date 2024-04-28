using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Collectable", menuName = "Collectable")]
public class Collectable : ScriptableObject
{
    public bool destroyable;
    public Sprite sprite;
    [TextArea(10, 10)] public string description;
    public int cost;
    [TextArea(30, 10)] public string content;
}

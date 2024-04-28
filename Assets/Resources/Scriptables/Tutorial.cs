using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tutorial", menuName = "Tutorial")]
public class Tutorial : ScriptableObject
{
    [TextArea(30, 10)] public string content;
}

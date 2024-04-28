using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableRename : MonoBehaviour
{
    [SerializeField] private TMP_Text collectableText;
    private string collectableName;

    private void Start()
    {
        collectableName = gameObject.name;
        collectableText.text = collectableName;
    }
}

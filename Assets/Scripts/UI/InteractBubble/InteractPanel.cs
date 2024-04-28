using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractPanel : MonoBehaviour
{
    [SerializeField] private string interactBtnText;
    [SerializeField] private TMP_Text btnText;

    // Start is called before the first frame update
    void Start()
    {
        if (btnText != null) {
            btnText.text = interactBtnText;
        }
        else {
            Debug.LogWarning("InteractBtnText GO not found");
        }
        
    }
}

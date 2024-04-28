using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignContinueButton : MonoBehaviour
{
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        GetComponent<Button>().onClick.AddListener(DialogManager.instance.ContinueStory);
    }
}

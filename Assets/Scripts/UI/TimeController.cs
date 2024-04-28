using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private GameObject zeroTimeScaleUIs;
    private bool atLeastOneActivated;
    // Start is called before the first frame update
    void Start()
    {
        atLeastOneActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        int activeChildren = ChildCountActive(zeroTimeScaleUIs);
        atLeastOneActivated = activeChildren != 0;
        if (atLeastOneActivated)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else Time.timeScale = 1f;
    }

    private int ChildCountActive(GameObject go)
    {
        int count = 0;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (go.transform.GetChild(i).gameObject.activeSelf)
            {
                count++;
            }
        }
        return count;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamepad : MonoBehaviour
{
    public bool isDisabled;
    public static Gamepad instance;

    private void Start()
    {
        if (instance != null) Debug.LogError("More than 1 instance");
        instance = this;
    }

    public bool GetSpaceDown()
    {
        if (this.isDisabled) return false;
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetEDown()
    {
        if (this.isDisabled) return false;
        return Input.GetKeyDown(KeyCode.E);
    }
    public bool GetEscDown()
    {
        if (this.isDisabled) return false;
        return Input.GetKeyDown(KeyCode.Escape);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimit : MonoBehaviour
{
    public enum Limits
    {
        noLimit = 0,
        limit30 = 30,
        limit60 = 60,
        limit80 = 80,
        limit120 = 120
    }

    public Limits limit;

    void Awake()
    {
        Application.targetFrameRate = (int) Limits.limit60;
    }
}

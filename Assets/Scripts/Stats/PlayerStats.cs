using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float movementSpeed = 15f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public Vector3 velocity = Vector3.zero;
}

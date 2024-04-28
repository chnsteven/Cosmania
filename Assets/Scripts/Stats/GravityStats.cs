using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GravityStats
{
    public bool applyGravity = true;
    [SerializeField] private float gravityForce = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;

    public float GetVerticalDisplacement(float deltaTime)
    {
        return gravityForce * gravityMultiplier * deltaTime;
    }
}

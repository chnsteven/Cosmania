using UnityEngine;
using System.Collections;

[System.Serializable]
public class NPCMovementStats
{
    [Header("Movement")]
    public float speed = 12f;
    public float turnSpeed = 3f;
    public float followRadius = 17f;
    [Header("Magic numbers")]
    public float timeUntilNextResponse = 1.5f;
    public float waypointThreshold = 0.5f;
}


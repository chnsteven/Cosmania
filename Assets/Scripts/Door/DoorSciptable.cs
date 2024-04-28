using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSciptable : MonoBehaviour
{
    public Door door;

    private void Start()
    {
        door = Resources.Load<Door>("Rooms/" + gameObject.name);
    }
}

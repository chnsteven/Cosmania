using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignSeats : MonoBehaviour
{
    [SerializeField] private Transform frontSeatGO;
    [SerializeField] private Transform backSeatGO;
    [SerializeField] private Transform frontSeat;
    [SerializeField] private Transform backSeat;

    // Start is called before the first frame update
    void Start()
    {
        frontSeatGO.SetPositionAndRotation(frontSeat.position, Quaternion.identity);
        backSeatGO.SetPositionAndRotation(backSeat.position, Quaternion.identity);
        frontSeatGO.parent = frontSeat;
        backSeatGO.parent = backSeat;
    }
}

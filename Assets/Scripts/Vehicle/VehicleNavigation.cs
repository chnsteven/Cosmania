using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleNavigation : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform destination;
    //[SerializeField] private Transform start;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //Debug.Log(navMeshAgent.isOnNavMesh);
        navMeshAgent.SetDestination(destination.position);
        //StartCoroutine(DriveIndefinitely());
    }
}

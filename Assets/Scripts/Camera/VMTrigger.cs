using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VMTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private int priority;
    private bool prioritized = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!prioritized) {
            virtualCamera.Priority++;
            prioritized = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (prioritized) {
            virtualCamera.Priority--;
            prioritized = false;
        }
        
    }
}

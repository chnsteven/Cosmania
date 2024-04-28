using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraMovement : MonoBehaviour
{
    private CinemachineFreeLook freeLook;

    private void Start()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.None)
        {
            freeLook.m_YAxis.m_InputAxisValue = 0f;
            freeLook.m_XAxis.m_InputAxisValue = 0f;
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisName = "";
        }
        else if(Cursor.lockState == CursorLockMode.Locked)
        {
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
        }
    }
}

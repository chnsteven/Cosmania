using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector2 rot;
    [SerializeField] private float rotationSpeed = 5f;
    private bool stopCameraMovement;
    // Start is called before the first frame update
    void Start()
    {
        rot.x = transform.localEulerAngles.x;
        rot.y = transform.localEulerAngles.y;
        //Debug.Log(rot.x + "," + rot.y);
        ContinueCameraMovement();
    }

    void Update()
    {
        if (SceneLoader.instance.isLoading) return;
        if (!stopCameraMovement && Cursor.lockState == CursorLockMode.Locked)
        {
            float horizontal = Input.GetAxisRaw("Mouse X");
            float vertical = Input.GetAxisRaw("Mouse Y");
            // TODO invert option
            rot.y += horizontal * rotationSpeed;
            rot.x -= vertical * rotationSpeed;
            transform.localEulerAngles = new Vector3(rot.x, rot.y, 0f);
        }
    }

    private void OnEnable()
    {
        DialogTrigger.EnterDialogMode += StopCameraMovement;
        DialogManager.EnterDialog += StopCameraMovement;
        DialogManager.EndOfStory += ContinueCameraMovement;
    }

    private void OnDisable()
    {
        DialogTrigger.EnterDialogMode -= StopCameraMovement;
        DialogManager.EnterDialog -= StopCameraMovement;
        DialogManager.EndOfStory -= ContinueCameraMovement;
    }

    private void ContinueCameraMovement()
    {
        stopCameraMovement = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void StopCameraMovement()
    {
        stopCameraMovement = true;
        Cursor.lockState = CursorLockMode.None;

    }
}

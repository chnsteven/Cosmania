using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CouchCamera : MonoBehaviour
{
    public static event Action<bool> CollectableHit;
    public static event Action<Collider> ShowConfirmMenu;
    [Header("Camera")]
    private new Camera camera;
    private Vector2 rot;
    private float rotationSpeed = 3f;
    [Header("Detection")]
    private LayerMask generalLayerMask;
    private LayerMask collectableLayerMask;
    private RaycastHit hit;
    private bool objIsHit = false;
    private float detectionRadius = 5f;
    [Header("LockOn")]
    // TODO make sure only saved once
    private Collider closestCollectable;
    [SerializeField] private float lockOnSpeed = 1.2f;
    private bool stopCameraMovement;
    [SerializeField] private float zoomInFactor = 0.8f;
    private float fov;
    [SerializeField] private AnimationCurve curve;
    private PlayerInteract playerInteract;


    private void Start()
    {
        stopCameraMovement = false;
        camera = GetComponent<Camera>();
        playerInteract = GameObject.Find("Player").GetComponent<PlayerInteract>();
        generalLayerMask = LayerMask.GetMask("Collectable", "Interactable", "Environment");
        collectableLayerMask = LayerMask.GetMask("Collectable");
        fov = camera.fieldOfView;
    }
    // Update is called once per frame
    void Update()
    {
        if (!stopCameraMovement)
        {
            float horizontal = Input.GetAxisRaw("Mouse X");
            float vertical = Input.GetAxisRaw("Mouse Y");
            // TODO invert option
            rot.y += horizontal * rotationSpeed;
            rot.x -= vertical * rotationSpeed;
            transform.localEulerAngles = new Vector3(rot.x, rot.y, 0f);
            DetectObjHit();
        }
    }

    private void DetectObjHit()
    {
        bool found = false;
        objIsHit = Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, generalLayerMask);
        if (objIsHit)
        {
            found = FindClosestCollectable();
        }
        if (!found) closestCollectable = null;
    }

    private void OnDrawGizmos()
    {
        if (!objIsHit) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hit.point, detectionRadius);
    }

    private void OnEnable()
    {
        CollectableInteract.LockOnCollectable += LockOnCollectable;
        PopupMenu.ExitInvestigation += ExitInvestigation;
        ConfirmMenu.Accepted += ContinueInvestigation;
        ConfirmMenu.Declined += ContinueInvestigation;
        PlayerInteract.ShowEndInvestigationPopup += StopCameraMovement;
        Countdown.InvestigationTimeout += StopCameraMovement;
        InventoryInteract.InventoryIsFull += StopCameraMovement;
        PopupMenu.ResumeInvestigation += ResumeInvestigation;
        PlayerInteract.GetClosestCollectable += GetClosestCollectable;
        DialogTrigger.EnterDialogMode += StopCameraMovement;
        DialogManager.EnterDialog += StopCameraMovement;
        DialogManager.EndOfStory += ResumeInvestigation;
    }

    private void OnDisable()
    {
        CollectableInteract.LockOnCollectable -= LockOnCollectable;
        PopupMenu.ExitInvestigation -= ExitInvestigation;
        ConfirmMenu.Accepted -= ContinueInvestigation;
        ConfirmMenu.Declined -= ContinueInvestigation;
        PlayerInteract.ShowEndInvestigationPopup -= StopCameraMovement;
        Countdown.InvestigationTimeout -= StopCameraMovement;
        InventoryInteract.InventoryIsFull += StopCameraMovement;
        PopupMenu.ResumeInvestigation -= ResumeInvestigation;
        PlayerInteract.GetClosestCollectable -= GetClosestCollectable;
        DialogTrigger.EnterDialogMode -= StopCameraMovement;
        DialogManager.EnterDialog -= StopCameraMovement;
        DialogManager.EndOfStory -= ResumeInvestigation;
    }

    private void ResumeInvestigation()
    {
        if (playerInteract.investigation)
        {
            stopCameraMovement = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
            
    }

    private void StopCameraMovement()
    {
        if (playerInteract.investigation)
        {
            //Debug.Log("Stop camera movement");
            stopCameraMovement = true;
            Cursor.lockState = CursorLockMode.None;
        }
            
    }

    private void ExitInvestigation()
    {
        gameObject.SetActive(false);
    }

    private void LockOnCollectable()
    {
        if (closestCollectable == null) return;
        StartCoroutine(Zoom(true));
        StartCoroutine(LockOnTarget());
    }

    private void ContinueInvestigation(Collider collectable, int cost)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(Zoom(false));
            stopCameraMovement = false;
        }
    }

    private bool FindClosestCollectable()
    {
        Collider[] colliders = Physics.OverlapSphere(hit.point, detectionRadius, collectableLayerMask);
        foreach (Collider currCollectable in colliders)
        {
            if (closestCollectable == null)
            {
                closestCollectable = currCollectable;
            }
            else
            {
                if (Vector3.Distance(currCollectable.transform.position, transform.position) <
                    Vector3.Distance(closestCollectable.transform.position, transform.position))
                {
                    closestCollectable = currCollectable;
                }
            }
        }
        CollectableHit?.Invoke(colliders.Length != 0);
        return colliders.Length != 0;
    }

    private IEnumerator LockOnTarget()
    {
        Gamepad.instance.isDisabled = true;
        stopCameraMovement = true;
        Quaternion lookRotation = Quaternion.LookRotation(closestCollectable.transform.position - transform.position);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * lockOnSpeed;
            float a = curve.Evaluate(t);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, a);

            yield return null;
        }
        rot.x = transform.localEulerAngles.x;
        rot.y = transform.localEulerAngles.y;
        ShowConfirmMenu?.Invoke(closestCollectable);
        closestCollectable = null;
        Gamepad.instance.isDisabled = false;
    }

    private IEnumerator Zoom(bool zoomIn)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * lockOnSpeed;
            float a = curve.Evaluate(t);
            float _fov;
            if (zoomIn)
            {
                _fov = Mathf.Lerp(fov, fov * zoomInFactor, a);
            }
            else
            {
                _fov = Mathf.Lerp(fov * zoomInFactor, fov, a);
            }

            camera.fieldOfView = _fov;
            yield return null;
        }
    }

    public GameObject GetClosestCollectable()
    {
        if (closestCollectable == null) return null;
        return closestCollectable.gameObject;
    }
}

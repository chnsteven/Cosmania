using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.Threading;

public class PlayerInteract : MonoBehaviour
{
    public static event Action ShowEndInvestigationPopup;
    public static event Action<bool, string, bool> InteractableFound;
    public static event Func<GameObject> GetClosestCollectable;
    [Header("Debug")]
    [SerializeField] private float interactRadius = 5f;
    private int interactableMask;
    [SerializeField] private bool interactableNearby;
    private bool timeout;

    public bool investigation;
    public bool idle;

    private void Start()
    {
        timeout = false;
        interactableMask = LayerMask.GetMask("Interactable");
    }

    private void OnEnable()
    {
        PlayerInvestigationState.PlayerEnterInvestigationState += EnterInvestigationState;
        PlayerInvestigationState.PlayerInInvestigationState += InInvestigationState;
        PlayerIdleState.PlayerInIdleState += InIdleState;
        PopupMenu.ExitInvestigation += GoIdle;
        Countdown.InvestigationTimeout += StopEndInvestigationPopup;
    }

    private void OnDisable()
    {
        PlayerInvestigationState.PlayerEnterInvestigationState -= EnterInvestigationState;
        PlayerInvestigationState.PlayerInInvestigationState -= InInvestigationState;
        PlayerIdleState.PlayerInIdleState -= InIdleState;
        PopupMenu.ExitInvestigation -= GoIdle;
        Countdown.InvestigationTimeout -= StopEndInvestigationPopup;
    }

    private void GoIdle()
    {
        PlayerStateManager.instance.SwitchState(PlayerStateManager.instance.IdleState);
    }

    private void StopEndInvestigationPopup()
    {
        timeout = true;
    }
    private void EnterInvestigationState()
    {
        investigation = true;
    }

    private void InInvestigationState()
    {
        investigation = true;
        idle = false;
        if (Gamepad.instance.GetSpaceDown() && !timeout)
        {
            ShowEndInvestigationPopup?.Invoke();
            return;
        }
        GameObject closestCollectable = GetClosestCollectable?.Invoke();
        if (closestCollectable == null)
        {
            InteractableFound?.Invoke(false, null, false);
            return;
        }
        closestCollectable.TryGetComponent(out CollectableInteract interactable);
        if (interactable.GetIsInteractable())
        {
            InteractableFound?.Invoke(true, interactable.GetInteractText(),
                interactable.GetIsInteractable());
            if (Gamepad.instance.GetEDown())
            {
                interactable.Interact(transform);
            }
        }
    }

    private void InIdleState()
    {
        investigation = false;
        idle = true;
        IInteractable interactable = GetInteractable();
        if (interactable == null)
        {
            InteractableFound?.Invoke(false, null, false);
            return;
        }
        else if (interactable.GetIsInteractable())
        {
            InteractableFound?.Invoke(true, interactable.GetInteractText(),
                interactable.GetIsInteractable());
            if (Gamepad.instance.GetEDown())
            {
                interactable.Interact(transform);
            }
        }
    }

    public IInteractable GetInteractable()
    {
        List<IInteractable> interactables = new();
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRadius, interactableMask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactables.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactables)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(interactable.GetTransform().position, transform.position) <
                    Vector3.Distance(closestInteractable.GetTransform().position, transform.position))
                {
                    closestInteractable = interactable;
                }
            }
        }
        return closestInteractable;
    }
}

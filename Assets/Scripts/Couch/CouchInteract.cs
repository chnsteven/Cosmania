using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CouchInteract : MonoBehaviour, IInteractable
{
    private IInteractableStruct s;
    private GameObject lockOnCollectable;
    private GameObject investigationUI;
    private GameObject player;
    private Transform spawnLocation;
    [SerializeField] private Transform sitPosition;

    private void Start()
    {
        s = new IInteractableStruct("Sit on couch", true);
        player = GameObject.Find("Player");
        lockOnCollectable = transform.Find("LockOnCollectable").gameObject;
        lockOnCollectable.SetActive(false);
        spawnLocation = transform.Find("SpawnLocation");

    }
    public string GetInteractText()
    {
        return s.interactText;
    }

    public bool GetIsInteractable()
    {
        return s.isInteractable;
    }

    public void GetNotified(string name, Ink.Runtime.Object value)
    {

    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        if (s.isInteractable)
        {
            //interactedThisCouch = true;
            s.isInteractable = false;
            PlayerStateManager.instance.stateManager.SwitchState(PlayerStateManager.instance.InvestigationState);
            lockOnCollectable.SetActive(true);
        }

    }

    private void OnEnable()
    {
        PopupMenu.ExitInvestigation += ExitInvestigation;
    }

    private void OnDisable()
    {
        PopupMenu.ExitInvestigation -= ExitInvestigation;
    }

    private void ExitInvestigation()
    {
        //if (interactedThisCouch)
        //    player.transform.position = spawnLocation.transform.position;
        gameObject.layer = LayerMask.NameToLayer("Environment");
    }
}

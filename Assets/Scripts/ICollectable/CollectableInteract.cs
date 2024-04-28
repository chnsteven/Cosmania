using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableInteract : MonoBehaviour, IInteractable, ICollectable
{
    public static event Action LockOnCollectable;
    public static event Action<Collectable> AddCollectableToInventory;
    [SerializeField] private Collectable collectable;
    private IInteractableStruct s;

    public void AddToInventory(Collider collider, int cost)
    {
        if (collider.gameObject.name == gameObject.name)
        {
            Debug.Log("Adding item to inventory");
            AddCollectableToInventory?.Invoke(collectable);
            if (collectable.destroyable)
            {
                Destroy(this.gameObject);
            }
            else
            {
                SetLayerRecursively(this.gameObject,
                    LayerMask.NameToLayer("Environment"));
            }
        }
    }

    public void SetIsInteractable(Collider collider, int cost)
    {
        if (collider.gameObject.name == gameObject.name)
        {
            s.isInteractable = true;
        }
    }

    public int GetTimeCost()
    {
        return collectable.cost;
    }

    public string GetName()
    {
        return collectable.name;
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public string GetDescriptionText()
    {
        return collectable.description;
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
        s.isInteractable = false;
        LockOnCollectable?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        string name = gameObject.name;
        s = new IInteractableStruct("Examine " + name, true);
    }

    private void OnEnable()
    {
        ConfirmMenu.Accepted += AddToInventory;
        ConfirmMenu.Declined += SetIsInteractable;
    }

    private void OnDisable()
    {
        ConfirmMenu.Accepted -= AddToInventory;
        ConfirmMenu.Declined -= SetIsInteractable;
    }
}

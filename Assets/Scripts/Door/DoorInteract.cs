using UnityEngine;
using TMPro;

public class DoorInteract : MonoBehaviour, IInteractable
{
    [Header("Animation")]
    private Animator animator;
    private bool isOpened = false;
    [Header("Interaction")]
    [SerializeField] private IInteractableStruct s;
    [SerializeField] private Door door;

    void Start()
    {
        door = GetComponentInParent<DoorSciptable>().door;
        if (door != null)
        {
            s = new IInteractableStruct("Open/Close Door", false);
        }
        animator = GetComponent<Animator>();
    }
    public string GetInteractText()
    {
        return s.interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        if (s.isInteractable)
        {
            animator.SetBool("Toggle", !isOpened);
            isOpened = !isOpened;
            if (isOpened)
            {
                DoorRoomTag.nextRoomScene = GetComponentInChildren<TMP_Text>().text;
                //Debug.Log("The next room scene to load is: " + DoorRoomTag.nextRoomScene);
            }
        }

    }

    public bool GetIsInteractable()
    {
        return s.isInteractable;
    }

    public void GetNotified(string name, Ink.Runtime.Object value)
    {
        if (name == "currentState" && value.ToString() == "nextScene" && door != null)
        {
            s.isInteractable = GameManager.instance.nextRoom == door.doorName;
            //if (s.isInteractable)
            //{
            //    Debug.Log(door.doorName + " is unlocked");
            //}
        }
    }

    private void OnEnable()
    {
        DialogVariables.NotifyVariableChanged += GetNotified;
    }

    private void OnDisable()
    {
        DialogVariables.NotifyVariableChanged -= GetNotified;
    }
}

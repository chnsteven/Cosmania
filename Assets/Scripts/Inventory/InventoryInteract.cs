using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryInteract : MonoBehaviour
{
    public static event Action<Collectable> ShowCollectableDescription;
    public static event Action InventoryIsFull;

    [SerializeField] private GameObject[] toggles;
    [SerializeField] private int index;

    void Start()
    {
        index = 0;
        toggles = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject toggle = transform.GetChild(i).gameObject;
            toggles[i] = transform.GetChild(i).gameObject;
        }
    }

    private void InventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Debug.Log("1 pressed");
            InventoryItemInteract interact = toggles[0].GetComponent<InventoryItemInteract>();
            ShowCollectableDescription?.Invoke(interact.collectable);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log("2 pressed");
            InventoryItemInteract interact = toggles[1].GetComponent<InventoryItemInteract>();
            ShowCollectableDescription?.Invoke(interact.collectable);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Debug.Log("3 pressed");
            InventoryItemInteract interact = toggles[2].GetComponent<InventoryItemInteract>();
            ShowCollectableDescription?.Invoke(interact.collectable);
        }
    }

    private void GetNotified(Collectable collectable) {
        InventoryItemInteract interact = toggles[index].GetComponent<InventoryItemInteract>();
        interact.collectable = collectable;
        GameObject bg = toggles[index].transform.Find("BG").gameObject;
        Image img = bg.GetComponent<Image>();
        img.sprite = collectable.sprite;
        index++;
        if (index >= toggles.Length)
        {
            StartCoroutine(InventoryisFullWithDelay());
        }
    }

    private IEnumerator InventoryisFullWithDelay()
    {
        yield return new WaitForSeconds(1f);
        InventoryIsFull?.Invoke();
    }


    private void OnEnable()
    {
        CollectableInteract.AddCollectableToInventory += GetNotified;
        PlayerIdleState.PlayerInIdleState += InventoryInput;
    }

    private void OnDisable()
    {
        CollectableInteract.AddCollectableToInventory -= GetNotified;
        PlayerIdleState.PlayerInIdleState -= InventoryInput;
    }

}

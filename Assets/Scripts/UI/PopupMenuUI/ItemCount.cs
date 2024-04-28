using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCount : MonoBehaviour
{
    [SerializeField] private TMP_Text availableItemCountText;
    [SerializeField] private TMP_Text pickupableItemCountText;
    private int availableItemCount;
    private int pickupableItemCount;

    void Start()
    {
        GameObject collectables = GameObject.Find("Collectables");
        GameObject inventory = GameObject.Find("Inventory");
        availableItemCount = collectables.transform.childCount;
        availableItemCountText.text = "Available items: " + availableItemCount;
        pickupableItemCount = inventory.transform.childCount;
        pickupableItemCountText.text = "Pickupable items: " + pickupableItemCount;
    }

    private void DecreaseCounts(Collectable collectable)
    {
        availableItemCount--;
        availableItemCountText.text = "Available items: " + availableItemCount;

        pickupableItemCount--;
        pickupableItemCountText.text = "Pickupable items: " + pickupableItemCount;
    }

    private void OnEnable()
    {
        CollectableInteract.AddCollectableToInventory += DecreaseCounts;
    }

    private void OnDisable()
    {
        CollectableInteract.AddCollectableToInventory -= DecreaseCounts;
    }
}

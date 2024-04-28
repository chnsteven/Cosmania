using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateInventory : MonoBehaviour
{
    [SerializeField] private int maxItems = 5;
    [SerializeField] private GameObject inventoryItem;
    [SerializeField] private ToggleGroup toggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        int i = transform.childCount + 1;
        while (i <= maxItems)
        {
            GameObject item = Instantiate(inventoryItem, transform);
            Toggle toggle = item.GetComponent<Toggle>();
            Text label = item.GetComponentInChildren<Text>();
            toggle.group = toggleGroup;
            label.text = i.ToString();
            i++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableUI : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject content;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void ShowContent(Collectable collectable)
    {
        if (collectable == null) return;
        TMP_Text text = content.GetComponent<TMP_Text>();
        RectTransform rect = content.GetComponent<RectTransform>();
        panel.SetActive(true);
        button.SetActive(true);
        text.text = collectable.content;
        rect.pivot = new Vector2(0.5f, 1f);
        title.text = collectable.name;
    }

    private void OnEnable()
    {
        InventoryInteract.ShowCollectableDescription += ShowContent;
    }

    private void OnDisable()
    {
        InventoryInteract.ShowCollectableDescription -= ShowContent;
    }

    public void HideContent()
    {
        panel.SetActive(false);
        button.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialInteract : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private GameObject popupPrefab;
    private bool btnInteractable = true;

    private void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = tutorial.name;
    }

    public void ToggleTutorial()
    {
        if (!btnInteractable || tutorial == null) return;
        btnInteractable = false;
        Transform popup = popupPrefab.transform.Find("PopupMenu");
        Transform content = popup.transform.Find("Content");
        Transform title = popup.transform.Find("Title");
        content.TryGetComponent<TMP_Text>(out TMP_Text contentText);
        title.TryGetComponent<TMP_Text>(out TMP_Text titleText);
        titleText.text = tutorial.name;
        contentText.text = tutorial.content;
        popupPrefab.SetActive(true);
    }
}

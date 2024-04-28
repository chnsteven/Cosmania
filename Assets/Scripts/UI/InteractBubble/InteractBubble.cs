using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class InteractBubble : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private GameObject interactPanel;

    private void Start()
    {
        interactPanel.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerInteract.InteractableFound += ShowInteractBubble;
        PopupMenu.ResumeInvestigation -= ResumeInvestigation;
    }

    private void OnDisable()
    {
        PlayerInteract.InteractableFound -= ShowInteractBubble;
        PopupMenu.ResumeInvestigation -= ResumeInvestigation;
    }

    private void ShowInteractBubble(bool interactableFound, string _interactText, bool _interactable)
    {
        if (interactableFound)
        {
            interactPanel.SetActive(true);
            interactText.text = _interactText;
            if (!_interactable)
            {
                interactText.fontStyle = FontStyles.Strikethrough;
            }
            else
            {
                interactText.fontStyle = FontStyles.Normal;
            }
        }
        else
        {
            interactPanel.SetActive(false);
        }
    }

    private void ResumeInvestigation()
    {
        interactPanel.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PopupMenu : MonoBehaviour
{
    public static event Action ResumeInvestigation;
    public static event Action ExitInvestigation;

    [SerializeField] private TMP_Text confirmText;
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private GameObject yesNoButton;
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameObject confirmMenu;

    private void OnEnable()
    {
        PlayerInteract.ShowEndInvestigationPopup += ShowEndInvestigationPopup;
        Countdown.InvestigationTimeout += ShowTimeoutPopup;
        InventoryInteract.InventoryIsFull += ShowFullPopup;
    }

    private void OnDisable()
    {
        PlayerInteract.ShowEndInvestigationPopup -= ShowEndInvestigationPopup;
        Countdown.InvestigationTimeout -= ShowTimeoutPopup;
        InventoryInteract.InventoryIsFull -= ShowFullPopup;
    }

    private void Show(bool enabled, bool yesNo)
    {
        popupMenu.SetActive(enabled);
        if (enabled)
        {
            if(yesNo)
            {
                yesNoButton.SetActive(true);
                okButton.SetActive(false);
            }
            else
            {
                yesNoButton.SetActive(false);
                okButton.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ShowEndInvestigationPopup()
    {
        if (confirmMenu.activeSelf) return;
        Show(true, true);
        string confirmMessage = "If you proceed to <color=red>yes</color>, " +
            "then you cannot investigate anymore." +
            "Do you wish to end investigation?\n";
        confirmText.text = confirmMessage;
    }

    private void ShowTimeoutPopup()
    {
        Show(true, false);
        string confirmMessage = "You used up your time.";
        confirmText.text = confirmMessage;
    }

    private void ShowFullPopup()
    {
        Show(true, false);
        string confirmMessage = "You have no space to pick up more items.";
        confirmText.text = confirmMessage;
    }

    public void Resume()
    {
        Show(false, true);
        ResumeInvestigation?.Invoke();
    }

    public void Exit()
    {
        Show(false, true);
        ExitInvestigation?.Invoke();
    }
}

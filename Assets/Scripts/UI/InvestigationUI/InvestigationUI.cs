using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InvestigationUI : MonoBehaviour
{
    public static event Action<Collider> SetConfirmMenu;

    [Header("Debug")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject confirmMenu;
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject bottomPanel;

    private PlayerInteract playerInteract;

    private void Start()
    {
        playerInteract = GameObject.Find("Player").GetComponent<PlayerInteract>();
    }
    private void OnEnable()
    {
        CouchCamera.ShowConfirmMenu += ShowConfirmMenu;
        ConfirmMenu.Accepted += HideConfirmMenu;
        ConfirmMenu.Declined += HideConfirmMenu;
        PlayerInvestigationState.ShowInvestigationUI += ShowInvestigationUI;
        PlayerInteract.ShowEndInvestigationPopup += HideCrosshair;
        PopupMenu.ResumeInvestigation += ShowCrosshair;
        Countdown.InvestigationTimeout += HideCrosshair;
        InventoryInteract.InventoryIsFull += HideCrosshair;
        DialogTrigger.EnterDialogMode += HideCrosshair;
        DialogManager.EnterDialog += HideCrosshair;
        DialogManager.EndOfStory += ShowCrosshair;
    }

    private void OnDisable()
    {
        CouchCamera.ShowConfirmMenu -= ShowConfirmMenu;
        ConfirmMenu.Accepted -= HideConfirmMenu;
        ConfirmMenu.Declined -= HideConfirmMenu;
        PlayerInvestigationState.ShowInvestigationUI -= ShowInvestigationUI;
        PlayerInteract.ShowEndInvestigationPopup -= HideCrosshair;
        PopupMenu.ResumeInvestigation -= ShowCrosshair;
        Countdown.InvestigationTimeout -= HideCrosshair;
        InventoryInteract.InventoryIsFull -= HideCrosshair;
        DialogTrigger.EnterDialogMode -= HideCrosshair;
        DialogManager.EnterDialog -= HideCrosshair;
        DialogManager.EndOfStory -= ShowCrosshair;
    }

    private void ShowInvestigationUI(bool active)
    {
        //confirmMenu.SetActive(!active);
        //Debug.Log("Show investigation UI");
        crosshair.SetActive(active);
        topPanel.SetActive(active);
        bottomPanel.SetActive(active);
    }

    private void ShowConfirmMenu(Collider collider)
    {
        //Debug.Log("Show Confirm Menu (InvestigationUI)");
        EnableConfirmMenu(true);
        SetConfirmMenu?.Invoke(collider);
    }

    private void HideConfirmMenu(Collider collider, int cost)
    {
        if (playerInteract.investigation)
            EnableConfirmMenu(false);
    }
    private void ShowCrosshair()
    {
        if (playerInteract.investigation)
            crosshair.SetActive(true);
    }

    private void HideCrosshair()
    {
        //Debug.Log("Hide crosshair");
        if (playerInteract.investigation)
            crosshair.SetActive(false);
    }

    void EnableConfirmMenu(bool enable)
    {
        if (enable)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        confirmMenu.SetActive(enable);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ConfirmMenu : MonoBehaviour
{
    public static event Action<Collider, int> Accepted;
    public static event Action<Collider, int> Declined;
    [Header("Debug")]
    [SerializeField] private TMP_Text confirmText;
    [SerializeField] private TMP_Text descriptionText;
    private Collider collectable;
    private int timeCost;

    void Start()
    {
    }

    private void OnEnable()
    {
        InvestigationUI.SetConfirmMenu += SetConfirmMenu;
    }

    private void OnDisable()
    {
        InvestigationUI.SetConfirmMenu -= SetConfirmMenu;
    }

    private void SetConfirmMenu(Collider _collectable)
    {
        if (_collectable == null) Debug.LogError("Confirm menu should not show");
        collectable = _collectable;
        ICollectable c = _collectable.gameObject.GetComponent<ICollectable>();
        SetConfirmText(c.GetName(), c.GetTimeCost());
        SetDescriptionText(c.GetDescriptionText());
    }

    private void SetConfirmText(string name, int _cost)
    {
        string _name = "<u>" + name + "</u>";
        string question = string.Format("Do you wish to pick up this {0}?\n " +
            "Pick up this {0} will cost {1} seconds.", _name, _cost);
        timeCost = _cost;
        confirmText.text = question;
    }

    private void SetDescriptionText(string _descriptionText)
    {
        descriptionText.text = _descriptionText;
    }

    public void Confirm()
    {
        Accepted?.Invoke(collectable, timeCost);
    }

    public void Decline()
    {
        Declined?.Invoke(collectable, timeCost);
    }
}

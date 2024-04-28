using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogTrigger : MonoBehaviour
{
    public static event Action EnterDialogMode;
    //public static event Action ExitDialogMode;

    [SerializeField] private float delay = 1f;
    [SerializeField] private GameObject nextTrigger;
    private DialogManager dialogManager;
    private Coroutine coroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (coroutine == null) coroutine = StartCoroutine(PlayDialog());
    }

    private IEnumerator PlayDialog()
    {
        EnterDialogMode?.Invoke();
        yield return new WaitForSeconds(delay);
        EnterDialogMode?.Invoke();
        dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        dialogManager.EnterDialogMode();
        Destroy(this.gameObject, 1f);
        if (nextTrigger != null)
            nextTrigger.SetActive(true);

    }
}

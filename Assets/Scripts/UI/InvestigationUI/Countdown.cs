using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Countdown : MonoBehaviour
{
    public static event Action InvestigationTimeout;

    [Header("Countdown")]
    [SerializeField] private TMP_Text countdownText;
    private float time;
    [SerializeField] private float timeLimit = 120f;
    [SerializeField] private GameObject confirmMenu;
    [SerializeField] private GameObject popupMenu;


    private void OnEnable()
    {
        ConfirmMenu.Accepted += DecreaseRemainingTime;
        PlayerInvestigationState.Countdown += StartCountdown;
        PopupMenu.ExitInvestigation += StopCountdown;
    }

    private void OnDisable()
    {
        ConfirmMenu.Accepted -= DecreaseRemainingTime;
        PlayerInvestigationState.Countdown -= StartCountdown;
        PopupMenu.ExitInvestigation -= StopCountdown;
    }

    private void DecreaseRemainingTime(Collider collider, int cost)
    {
        if (time - cost > 0f)
        {
            time -= cost;
        }
        else
        {
            time = 0f;
            countdownText.text = "Time remaining: " +
                string.Format("{0:00.00}", time);
            StopCoroutine(CountdownCoroutine());
            InvestigationTimeout?.Invoke();
        }
    }


    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private void StopCountdown()
    {
        StopAllCoroutines();
    }

    private IEnumerator CountdownCoroutine()
    {
        time = timeLimit;
        while (time > 0f)
        {
            if (confirmMenu.activeSelf || popupMenu.activeSelf)
                yield return new WaitForSeconds(0.5f);
            else
            {
                countdownText.text = "Time remaining: " +
                    string.Format("{0:00.00}", time);
                time -= Time.deltaTime;
                time = Mathf.Clamp(time, 0f, Mathf.Infinity);

            }
            yield return null;
        }
        time = 0f;
        countdownText.text = "Time remaining: " +
            string.Format("{0:00.00}", time);
        InvestigationTimeout?.Invoke();
    }
}

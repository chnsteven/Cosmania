using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelector : MonoBehaviour
{
    public float timer = 0f;
    public float stayAtLeast = 1f;
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        if (timer < stayAtLeast) return;
        GameManager.instance.SelectOption(transform.parent.name);
        Destroy(this);
    }
    private void OnTriggerExit(Collider other)
    {
        timer = 0f;
    }
}

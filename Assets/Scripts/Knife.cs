using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("StopCutting", false);
    }
    private void OnEnable()
    {
        DialogTrigger.EnterDialogMode += StopCutting;
        //DialogManager.EndOfStory;
    }

    private void OnDisable()
    {
        DialogTrigger.EnterDialogMode -= StopCutting;

    }

    private void StopCutting()
    {
        animator.SetBool("StopCutting", true);
    }
}

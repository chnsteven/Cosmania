using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static Warning instance;
    [SerializeField] private GameObject warningGO;

    // Start is called before the first frame update
    void Start()
    {
       
        animator = warningGO.GetComponent<Animator>();
        warningGO.SetActive(false);
        instance = this;
    }

    public void Show()
    {
        if (animator.isActiveAndEnabled)
            animator.SetBool("Show", true);
    }
    public void Hide()
    {
        if (animator.isActiveAndEnabled)
            animator.SetBool("Show", false);
    }
}

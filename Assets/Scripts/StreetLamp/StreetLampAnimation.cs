using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLampAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Blink", -1, Random.Range(0f, 1f));
    }
}

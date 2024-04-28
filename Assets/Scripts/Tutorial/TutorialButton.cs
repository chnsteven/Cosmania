using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("Glow", 1) == 1)
        {
            animator.SetTrigger("Glow");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffGlow()
    {
        PlayerPrefs.SetInt("Glow", 0);
        animator.ResetTrigger("Glow");
    }
}

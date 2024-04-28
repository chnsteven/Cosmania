using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour
{

    private Animator animator;
    private PlayerMovement movement;
    private bool isDead = false;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isDead)
        {
            isDead = true;
            StartCoroutine(DeadCoroutine());
        }
    }

    public void Dead()
    {
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DeadCoroutine()
    {
        if (animator == null || movement == null) yield return null;
        if (!animator.isActiveAndEnabled) animator.enabled = true;
        animator.SetTrigger("Die");
        movement.enabled = false;
        yield return new WaitUntil(()=>!AnimatorIsPlaying());
        GameManager.instance.GameOver();
    }

    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}


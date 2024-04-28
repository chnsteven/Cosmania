using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePosition;
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] private GameObject explosionEffect;
    private float timer = 0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Attack());
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }

    public IEnumerator Attack()
    {
        while (timer < 100f && !NPCMovement.instance.isFinished)
        {
            animator.SetTrigger("Fire");
            Instantiate(bullet, firePosition.position, firePosition.rotation);
            GameObject effect = Instantiate(explosionEffect, firePosition.position, firePosition.rotation);
            Destroy(effect, 2f);
            yield return new WaitForSeconds(Random.Range(0.5f * fireCooldown, 1.5f * fireCooldown));
        }

        StopCoroutine(Attack());
        this.enabled = false;

    }
}

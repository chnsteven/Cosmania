using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLookAt : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private NPCLookAtStats stats;

    private Coroutine lookCoroutine;
    void Start()
    {
        GameObject.FindWithTag("Player").TryGetComponent<Transform>(out playerTransform);
    }
    private void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogError("playerTransform is null");
            return;
        }
        float dist = Vector3.Distance(playerTransform.position, transform.position);
        if (dist < stats.detectionRadius && !NPCMovement.instance.isMoving)
        {
            LookAtPlayer();
        }
    }

    public void LookAtPlayer()
    {
        if (lookCoroutine != null)
        {
            StopCoroutine(SlerpLookAtRotation());
        }
        lookCoroutine = StartCoroutine(SlerpLookAtRotation());
    }

    private IEnumerator SlerpLookAtRotation()
    {
        Quaternion lookRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        float time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * stats.rotationSpeed;
            yield return null;
        }
    }
}

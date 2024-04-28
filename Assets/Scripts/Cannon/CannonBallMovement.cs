using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallMovement : MonoBehaviour
{
    [SerializeField] private CannonBallStats stats;
    private float currTravelDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {
        while (currTravelDistance < stats.maxTravelDistance)
        {
            Vector3 moveDir = Vector3.forward;
            currTravelDistance += (Time.deltaTime * stats.speed * moveDir).magnitude;
            transform.Translate(Time.deltaTime * stats.speed * moveDir);
            yield return null;
        }

        Destroy(gameObject);
        StopCoroutine(Move());
    }
}

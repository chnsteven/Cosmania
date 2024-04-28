using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public static NPCMovement instance;
    [SerializeField] private NPCMovementStats stats;
    [Header("Airwall")]
    [SerializeField] private GameObject airwall;
    private GameObject player;
    private Transform playerTransform;
    public bool isMoving = false;
    public bool isFinished = false;

    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    public void FollowPath()
    {
        //Debug.Log("Follow Path");
        StartCoroutine(FollowPathCoroutine());
    }
    public IEnumerator FollowPathCoroutine()
    {
        int index = 0;
        while (index < Waypoints.points.Length)
        {
            if (PlayerIsCloseEnough())
            {
                isMoving = true;
                Transform nextWaypoint = Waypoints.points[index];
                if (ReachedNextWaypoint(nextWaypoint)) index++;
                else
                {
                    Vector3 moveDir = nextWaypoint.position - transform.position;
                    Move(moveDir);
                    Rotate(moveDir);
                }
                yield return null;
            }
            else
            {
                isMoving = false;
                yield return new WaitForSeconds(stats.timeUntilNextResponse);
            }
        }
        Debug.Log("Done");
        DestroyAirwall();
        isMoving = false;
        isFinished = true;
    }

    private bool ReachedNextWaypoint(Transform nextWaypoint)
    {
        float distToNextWaypoint = Vector3.Distance(transform.position, nextWaypoint.position);
        return distToNextWaypoint < stats.waypointThreshold;
    }
    private bool PlayerIsCloseEnough()
    {
        float distToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        return distToPlayer < stats.followRadius;
    }

    void Move(Vector3 dir)
    {
        transform.Translate(Time.deltaTime * stats.speed * dir.normalized, Space.World);
        if (airwall == null) return;
        airwall.transform.Translate(Time.deltaTime * stats.speed *
            new Vector3(0f, 0f, dir.normalized.z), Space.World);
    }

    void Rotate(Vector3 dir)
    {
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * stats.turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void DestroyAirwall()
    {
        if (airwall == null) return;
        Destroy(airwall);
        return;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.followRadius);
    }
}

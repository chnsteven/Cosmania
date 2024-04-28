using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirwallCollision : MonoBehaviour
{
    [SerializeField] private float timer = 0f;
    public LayerMask m_LayerMask;
    [SerializeField] private Warning warning;

    void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        if (hitColliders == null) return;
        if (hitColliders.Length == 1) timer += Time.deltaTime;
        else
        {
            warning.Hide();
            timer = 0f;
            return;
        }

        if (timer > 0.3f)
        {
            warning.Show();
            return;
        }


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}

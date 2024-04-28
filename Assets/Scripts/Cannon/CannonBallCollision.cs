using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallCollision : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -1.5f);
    private void OnTriggerEnter(Collider other)
    {
        if (effectPrefab == null) return;

        if (other.gameObject.CompareTag("NPC"))
        {
            Destroy(this.gameObject);
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(effectPrefab, gameObject.transform.position + offset, gameObject.transform.rotation);
            Destroy(effect, 2f);
            Destroy(this.gameObject);
            KillPlayer(other.gameObject);
            return;
        }
    }

    private void KillPlayer(GameObject player)
    {
        PlayerStates states = player.GetComponent<PlayerStates>();
        states.Dead();
    }
}

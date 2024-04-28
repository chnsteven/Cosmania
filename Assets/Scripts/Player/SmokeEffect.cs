using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    [SerializeField] private bool playerIsMoving;
    private CharacterController controller;
    private ParticleSystem particle;




    private void Start()
    {
        controller = GetComponentInParent<CharacterController>();
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();
    }
    private void FixedUpdate()
    {
        if (controller == null) return;
        playerIsMoving = controller.velocity.magnitude > 0f;
        if (playerIsMoving) particle.Play();
        else particle.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    public ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        if (effect != null) effect.Play();
    }
}

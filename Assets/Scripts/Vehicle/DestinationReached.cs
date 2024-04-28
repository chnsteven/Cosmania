using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationReached : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.instance.LoadScene(nextScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void SkipCutScene()
    {
        SceneLoader.instance.LoadScene(nextScene);
        Destroy(this);
    }
}

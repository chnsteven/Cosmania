using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class BranchOffTimeline : MonoBehaviour
{
    [SerializeField] private string branchOffTarget;
    [SerializeField] private PlayableDirector playable; 

    private void OnEnable()
    {
        playable.stopped += BranchOff;
    }

    //private void OnDisable()
    //{
    //    playable.stopped -= BranchOff;
    //}

    private void BranchOff(PlayableDirector playableDirector)
    {
        SceneLoader.instance.LoadScene(branchOffTarget);
        playable.stopped -= BranchOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

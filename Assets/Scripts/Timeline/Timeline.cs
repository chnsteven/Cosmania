using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    //private string choice;
    [SerializeField] private GameObject investigate;
    [SerializeField] private GameObject noInvestigate;
    private void OnEnable()
    {
        DialogVariables.NotifyVariableChanged += GetNotified;

    }

    private void OnDisable()
    {
        DialogVariables.NotifyVariableChanged -= GetNotified;

    }

    private void GetNotified(string name, Ink.Runtime.Object value)
    {
        if (name == "currentState" && value.ToString() == "investigate")
        {
            investigate.SetActive(true);
            //investigate.TryGetComponent<PlayableDirector>(out PlayableDirector playable);
            //playable.Play();

            //choice = "investigate";
        }
        else if (name == "currentState" && value.ToString() == "noInvestigate")
        {
            noInvestigate.SetActive(true);
            //noInvestigate.TryGetComponent<PlayableDirector>(out PlayableDirector playable);
            //playable.playableGraph.GetRootPlayable(0).SetSpeed(1);
            //playable.Play();
            //choice = "noInvestigate";
        }
    }

    //public void BranchOff()
    //{
    //    if (choice == "investigate")
    //    {
    //        investigate.Play();
    //    }
    //    else if (choice == "noInvestigate")
    //    {
    //        noInvestigate.Play();
    //    }
    //}
}

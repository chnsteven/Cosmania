using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;
using System;

public class DialogVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }
    public static event Action<string, Ink.Runtime.Object> NotifyVariableChanged;

    public DialogVariables(TextAsset loadGlobalsJSON)
    {
        // create the story
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            //Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
            NotifyVariableChanged?.Invoke(name, value);
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    public void VariableChanged(string name, Ink.Runtime.Object value)
    {
        //Debug.Log("Variable changed: " + name + " = " + value);
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
            // Observer pattern, interactables are notified 
            NotifyVariableChanged?.Invoke(name, value);
        }
        
    }

    private void VariablesToStory(Story story) {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables){
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
